const BASE_URL = "/api";

// TODO: Add a generic error handler
export const httpGet = async <T>(
  path: string,
  init?: RequestInit
): Promise<T> => {
  const response = await fetch(`${BASE_URL}${path}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      ...(init?.headers || {}),
    },
    ...init,
  });
  if (!response.ok) {
    throw new Error(`HTTP ${response.status} - ${response.statusText}`);
  }
  return (await response.json()) as T;
};

export const httpPost = async <TResponse, TBody = unknown>(
  path: string,
  body: TBody,
  init?: RequestInit
): Promise<TResponse> => {
  const response = await fetch(`${BASE_URL}${path}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      ...(init?.headers || {}),
    },
    body: JSON.stringify(body),
    ...init,
  });
  if (!response.ok) {
    const text = await response.text();
    throw new Error(
      `HTTP ${response.status} - ${response.statusText}${text ? `: ${text}` : ""}`
    );
  }
  try {
    return (await response.json()) as TResponse;
  } catch {
    return undefined as unknown as TResponse;
  }
};
