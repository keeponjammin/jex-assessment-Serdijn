<template>
  <div class="app">
    <CompanyList ref="companyListRef" />
    <hr />
    <AddVacancyForm :companies="allCompanies" @created="onVacancyCreated" />
  </div>
</template>

<script setup lang="ts">
import CompanyList from "@/components/CompanyList.vue";
import AddVacancyForm from "@/components/AddVacancyForm.vue";
import { useCompaniesWithVacancies } from "@/composables/useCompanies";
import { ref } from "vue";

const { companies: allCompanies, reload } = useCompaniesWithVacancies();

const companyListRef = ref<InstanceType<typeof CompanyList> | null>(null);

// reload global store
const onVacancyCreated = () => {
  reload();
  companyListRef.value?.reload?.();
};
</script>

<style lang="scss">
@use "@/style/main.scss" as *;
</style>
