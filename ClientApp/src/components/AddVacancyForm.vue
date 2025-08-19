<template>
  <form class="vacancy-form" @submit.prevent="onSubmit">
    <div class="field">
      <label for="title">Titel</label>
      <input id="title" v-model="title" type="text" @blur="titleBlur" />
      <span class="error" v-if="titleMeta.touched && titleError">{{
        titleError
      }}</span>
    </div>

    <div class="field">
      <label for="description">Omschrijving</label>
      <textarea
        id="description"
        v-model="description"
        rows="4"
        @blur="descriptionBlur"
      ></textarea>
      <span class="error" v-if="descriptionMeta.touched && descriptionError">{{
        descriptionError
      }}</span>
    </div>

    <div class="field">
      <label for="companyId">Bedrijf</label>
      <select id="companyId" v-model="companyId" @blur="companyIdBlur">
        <option value="" disabled>Kies een bedrijf...</option>
        <option v-for="c in companies" :key="c.id" :value="c.id">
          {{ c.name }}
        </option>
      </select>
      <span class="error" v-if="companyIdMeta.touched && companyIdError">{{
        companyIdError
      }}</span>
    </div>

    <div class="actions">
      <button type="submit" :disabled="submitting">Vacature toevoegen</button>
    </div>
  </form>
</template>

<script setup lang="ts">
import { useVacancyForm } from "@/composables/useVacancyForm";
import type { CompanyDto } from "@/types/generated-api";

const { companies } = defineProps<{ companies: CompanyDto[] }>();
const emit = defineEmits<{ (e: "created"): void }>();

const {
  submitting,
  title,
  titleError,
  titleMeta,
  titleBlur,
  description,
  descriptionError,
  descriptionMeta,
  descriptionBlur,
  companyId,
  companyIdError,
  companyIdMeta,
  companyIdBlur,
  onSubmit,
} = useVacancyForm(() => emit("created"));
</script>
