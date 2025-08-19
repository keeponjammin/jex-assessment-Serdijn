<template>
  <section class="company-list">
    <header class="company-list__header">
      <h2>Bedrijven met actieve vacatures</h2>
    </header>

    <div v-if="loading" class="state">Laden...</div>
    <div v-else-if="error" class="state state--error">{{ error }}</div>
    <ul v-else class="companies">
      <li
        v-for="company in companiesWithActiveVacancies"
        :key="company.id"
        class="company"
      >
        <h3 class="company__name">{{ company.name }}</h3>
        <ul class="vacancies">
          <li v-for="vac in company.vacancies" :key="vac.id" class="vacancy">
            {{ vac.title }}
          </li>
        </ul>
      </li>
    </ul>
  </section>
</template>

<script setup lang="ts">
import { useCompaniesWithVacancies } from "@/composables/useCompanies";

const { loading, error, companiesWithActiveVacancies, reload } =
  useCompaniesWithVacancies();

// Expose a method to allow parent components to trigger a reload
defineExpose({ reload });
</script>
