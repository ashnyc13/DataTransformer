<template>
  <div id="home-view-container">
    <h1>Data Transformer</h1>

    <b-form-select
      v-model="selectedPipeline"
      :options="allPipelines"
    ></b-form-select>
    <p></p>
    <b-container v-if="selectedPipeline">
      <b-row>
        <b-col md="8" cols="12">
          <PipelineExecuter :selectedPipeline="selectedPipeline" />
        </b-col>
        <b-col md="4" cols="12">
          <PipelineManagerPanel :selectedPipeline="selectedPipeline" />
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import PipelineExecuter from "@/components/PipelineExecuter";
import PipelineManagerPanel from "@/components/PipelineManagerPanel";

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
    PipelineManagerPanel,
  },
};
</script>
