export type HelloResponse = {
  message: string
}

export type ErrorResponseBody = {
  message: string
}

export class ApiError extends Error {
  status: number;

  constructor(status: number, message: string) {
    super(message);
    this.status = status;
  }
}
