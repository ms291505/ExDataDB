using ExData.Data;
using Microsoft.EntityFrameworkCore;

namespace ExData.Api.Endpoints;

public static class ExerciseEndpoints
{
    public static RouteGroupBuilder MapExerciseEndpoints(this RouteGroupBuilder api)
    {
        var exerciseGroup = api.MapGroup("/exercises");

        exerciseGroup.MapGet("/", GetAll);
        exerciseGroup.MapGet("/paginated", GetPaginated);
        exerciseGroup.MapGet("/cursor-pagination", GetCursor);

        exerciseGroup.MapGet("/{exerciseId:int}", GetById);

        return exerciseGroup;
    }

    private static async Task<IResult> GetAll(ExDataDb db)
    {
        var exercises = await db.Exercises.ToListAsync();

        var responses = exercises.Select(e => new ExerciseResponse
        {
            Id = e.Id,
            Name = e.Name,
            Force = e.Force,
            Mechanic = e.Mechanic,
            PrimaryMuscles = e.PrimaryMuscles,
            SecondaryMuscles = e.SecondaryMuscles,
            Equipment = e.Equipment,
            Category = e.Category,
        });

        return TypedResults.Ok(responses);
    }

    private static async Task<IResult> GetPaginated(ExDataDb db, int page = 1, int pageSize = 25)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = db.Exercises.AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(e => e.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new ExerciseResponse
            {
                Id = e.Id,
                Name = e.Name,
                Force = e.Force,
                Mechanic = e.Mechanic,
                PrimaryMuscles = e.PrimaryMuscles,
                SecondaryMuscles = e.SecondaryMuscles,
                Equipment = e.Equipment,
                Category = e.Category,
            })
            .ToListAsync();

        return TypedResults.Ok(
            new PagedResponse<ExerciseResponse>(items, page, pageSize, totalCount)
        );
    }

    private static async Task<IResult> GetCursor(
        ExDataDb db,
        int? lastId = null,
        string? lastName = null,
        int pageSize = 25,
        CancellationToken cancellationToken = default
    )
    {
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = db
            .Exercises.AsNoTracking()
            .OrderBy(e => e.Name)
            .ThenBy(e => e.Id)
            .AsQueryable();

        if (lastId is not null && lastName is not null)
        {
            query = query.Where(e =>
                string.Compare(e.Name, lastName) > 0 || (e.Name == lastName && e.Id > lastId)
            );
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Take(pageSize + 1)
            .Select(e => new ExerciseResponse
            {
                Id = e.Id,
                Name = e.Name,
                Force = e.Force,
                Mechanic = e.Mechanic,
                PrimaryMuscles = e.PrimaryMuscles,
                SecondaryMuscles = e.SecondaryMuscles,
                Equipment = e.Equipment,
                Category = e.Category,
            })
            .ToListAsync(cancellationToken);

        var hasMore = items.Count > pageSize;
        if (hasMore)
        {
            items.RemoveAt(items.Count - 1);
        }

        return TypedResults.Ok(
            new CursorPageResponse<ExerciseResponse>(items, hasMore, pageSize, totalCount)
        );
    }

    public static async Task<IResult> GetById()
    {
        throw new NotImplementedException();
    }

    public sealed record PagedResponse<T>(
        IReadOnlyList<T> Items,
        int Page,
        int PageSize,
        int TotalCount
    );

    public sealed record CursorPageResponse<T>(
        IReadOnlyList<T> Items,
        bool hasMore,
        int PageSize,
        int TotalCount
    );
}
