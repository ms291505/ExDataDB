const API_BASE: string = "";


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

export async function hello(): Promise<string> {
  const response = await fetch(createPath(), {
    method: "GET",
    credentials: "include"
  });

  if (!response.ok) throw new Error("Failed the hello handshake.");

  return response.json();
}
