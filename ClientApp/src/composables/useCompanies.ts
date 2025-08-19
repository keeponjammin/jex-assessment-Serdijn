import { ref, computed, onMounted } from "vue";
import { fetchCompanies } from "@/services/companies.service";
import type { CompanyDto } from "@/types/generated-api";

export const useCompaniesWithVacancies = () => {
  const loading = ref(false);
  const error = ref<string | null>(null);
  const companies = ref<CompanyDto[]>([]);

  //Filter out companies without vacancies
  const companiesWithActiveVacancies = computed<CompanyDto[]>(() =>
    companies.value.filter((c) => (c.vacancies?.length ?? 0) > 0)
  );

  const load = async () => {
    loading.value = true;
    error.value = null;
    try {
      const companiesResp = await fetchCompanies();
      companies.value = companiesResp;
    } catch (e) {
      error.value = e instanceof Error ? e.message : String(e);
    } finally {
      loading.value = false;
    }
  };

  onMounted(load);

  return {
    loading,
    error,
    companies,
    companiesWithActiveVacancies,
    reload: load,
  };
};
