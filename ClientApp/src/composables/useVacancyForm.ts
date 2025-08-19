import { ref, watch } from "vue";
import { useForm, useField } from "vee-validate";
import * as yup from "yup";
import { createVacancy } from "@/services/vacancies.service";
import type { CreateVacancyDto } from "@/types/generated-api";

export const useVacancyForm = (onCreated?: () => void) => {
  const submitting = ref(false);

  const schema = yup.object({
    title: yup.string().required().min(3).max(200),
    description: yup.string().required().min(10).max(1000),
    companyId: yup.string().required(),
  });

  const { handleSubmit, resetForm } = useForm<CreateVacancyDto>({
    validationSchema: schema,
    validateOnMount: false,
    initialValues: { title: "", description: "", companyId: "" },
  });

  const {
    value: title,
    errorMessage: titleError,
    meta: titleMeta,
    handleBlur: titleBlur,
    validate: validateTitle,
  } = useField<string>("title", undefined, { validateOnValueUpdate: false });

  const {
    value: description,
    errorMessage: descriptionError,
    meta: descriptionMeta,
    handleBlur: descriptionBlur,
    validate: validateDescription,
  } = useField<string>("description", undefined, {
    validateOnValueUpdate: false,
  });

  const {
    value: companyId,
    errorMessage: companyIdError,
    meta: companyIdMeta,
    handleBlur: companyIdBlur,
    validate: validateCompanyId,
  } = useField<string>("companyId", undefined, {
    validateOnValueUpdate: false,
  });

  // After first blur (touched), validate on subsequent changes
  watch(title, () => {
    if (titleMeta.touched) validateTitle();
  });
  watch(description, () => {
    if (descriptionMeta.touched) validateDescription();
  });
  watch(companyId, () => {
    if (companyIdMeta.touched) validateCompanyId();
  });

  const onSubmit = handleSubmit(async (vals) => {
    submitting.value = true;
    try {
      await createVacancy(vals);
      resetForm();
      onCreated?.();
    } finally {
      submitting.value = false;
    }
  });

  return {
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
  };
};
