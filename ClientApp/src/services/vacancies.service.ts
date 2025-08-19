import { httpPost } from "./http";
import type { CreateVacancyDto } from "@/types/generated-api";

export const createVacancy = async (
  payload: CreateVacancyDto
): Promise<void> => {
  await httpPost<void, CreateVacancyDto>("/vacancies", payload);
};


