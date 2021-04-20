<template>
  <div>
    <h4>Execute selected pipeline</h4>
    <b-form-textarea
      id="inputTextArea"
      v-model="input"
      placeholder="Type/paste input data here"
      rows="3"
      max-rows="6"
    ></b-form-textarea>

    <p>
      <b-button @click="executeSelectedPipeline">Execute Pipeline</b-button>
      |
      <b-button @click="copyOutputToInput">Copy to input</b-button>
      |
      <b-form-checkbox inline v-model="isInReverse"
        >Execute pipeline in reverse</b-form-checkbox
      >
    </p>

    <b-form-textarea
      id="outputTextArea"
      v-model="output"
      placeholder="Output data will be available here after processing"
      rows="3"
      max-rows="6"
      :readonly="true"
    ></b-form-textarea>
  </div>
</template>

<script>
export default {
  data() {
    return {
      input: "",
      output: "",
      isInReverse: false,
    };
  },
  methods: {
    executeSelectedPipeline() {
      // TODO: Execute selected pipeline
      this.output = "Output from pipeline";
    },
    copyOutputToInput() {
      this.input = this.output;
    },
  },
};
</script>