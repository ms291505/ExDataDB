import { ApiError, type HelloResponse, type ErrorResponseBody } from "../library/responses.ts";

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

function handleApiError(): void {

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
    const body: ErrorResponseBody = await response.json();
    console.log(body);
    console.log(body.message);
    throw new ApiError(response.status, body.message ?? "Unkown error.");
  }

  return response.json();
}
