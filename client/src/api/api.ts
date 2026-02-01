import type { Hello } from "../types";


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

export async function hello(): Promise<Hello> {
  const response = await fetch(createPath("hello"), {
    method: "GET",
    credentials: "include"
  });
  console.log("VITE_API_URL:", import.meta.env.VITE_API_URL);

  if (!response.ok) throw new Error("Failed the hello handshake.");

  console.log(response);

  return response.json();
}
