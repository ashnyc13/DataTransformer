export default {
    getAllPipelinesDropdownValues(state) {
        var items = state.allPipelines.map(pipeline => {
            return { text: pipeline.name, value: pipeline.id };
        });
        items.unshift({ text: "Please select a pipeline", value: null });
        return items;
    }
}