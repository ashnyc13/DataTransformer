<template>
  <div class="home-view-container">
    <h1>Data Transformer</h1>

    <b-form-select
      v-model="selectedPipeline"
      :options="allPipelines"
    ></b-form-select>
    <p></p>
    <PipelineExecuter
      :selectedPipeline="selectedPipeline"
      v-if="selectedPipeline"
    />
  </div>
</template>

<script>
import PipelineExecuter from "@/components/PipelineExecuter";

export default {
  name: "Home",
  data() {
    return {
      selectedPipeline: null,
      allPipelines: [
        { value: null, text: "Please select a pipeline" },
        { value: "Ascii + Base64", text: "Ascii + Base64" },
      ],
    };
  },
  components: {
    PipelineExecuter,
  },
};
</script>
