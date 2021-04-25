export default {
  savePipeline(state, pipelineData) {
    pipelineData.id = state.allPipelines.length + 1;
    state.allPipelines.push(pipelineData);
  }
};
