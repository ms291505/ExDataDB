import { Box, Typography } from "@mui/material";
import { getExercises } from "../../api/api";
import { useQuery } from "@tanstack/react-query";
import type { Exercise } from "../../library/types";

export default function ExerciseLibrary() {

  const exercises = useQuery({
    queryKey: ["exercises"], queryFn: getExercises
  });

  return (
    <Box>
      <Typography variant="h2">The ExData Library</Typography>
      {exercises.data?.map((e: Exercise) => (
        <li key={e.id}>{e.name}</li>
      ))}
    </Box>
  )
}
