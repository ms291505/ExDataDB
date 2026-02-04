import { ApiError, type HelloResponse, type ErrorResponseBody } from "../library/responses.ts";
import { type PagedResponse, type Exercise } from "../library/types.ts";

const API_BASE = import.meta.env.VITE_API_URL;

const createPath = (segment: string = "") => {
  if (segment == "") return `${API_BASE}/`;
  let clean = segment;
  if (clean.charAt(0) === "/") {
    clean = clean.slice(1);
  }

  return (
    `${API_BASE}/${segment}/`
  );
}

async function handleApiError(response: Response): Promise<never> {
  let message = response.statusText;
  const body = (await response.json()) as Partial<ErrorResponseBody>;
  if (typeof body.message === "string" && body.message !== "") {
    message = body.message;
  }
  throw new ApiError(response.status, body.message ?? response.statusText);
}

export async function hello(): Promise<HelloResponse> {
  const response = await fetch(createPath("hello"), {
    method: "GET",
    credentials: "include"
  });

  if (!response.ok) throw new Error("Failed the hello handshake.");

  return response.json();
}

export async function notFound() {
  const response = await fetch(createPath("not-found"), {
    method: "GET",
    credentials: "include"
  });

  console.log(response);

  if (!response.ok) {
    await handleApiError(response);
  }

  return response.json();
}

export async function getExercises() {
  const response = await fetch(createPath("exercises"), {
    method: "GET",
    credentials: "include"
  });

  console.log(response.json());

  if (!response.ok) {
    await handleApiError(response);
  }
}

export async function getExercisesPaginated(page: number, pageSize: number): Promise<PagedResponse<Exercise>> {
  const response = await fetch(`http://localhost:5192/api/exercises/paginated?page=${page}&pageSize=${pageSize}`, {
    method: "GET",
    credentials: "include"
  });

  return response.json();
}
