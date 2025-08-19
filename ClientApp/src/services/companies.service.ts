import { httpGet } from "./http";
import type { CompanyDto } from "@/types/generated-api";

export const fetchCompanies = async (): Promise<CompanyDto[]> => {
  return httpGet<CompanyDto[]>("/companies");
};
