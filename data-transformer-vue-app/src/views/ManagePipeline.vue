<template>
  <div id="manage-view-container">
    <h1>{{ op }} pipeline</h1>
    <b-form>
      <b-form-group
        id="input-group-1"
        label="Pipeline Name:"
        label-for="inputPipelineName"
      >
        <b-form-input
          id="inputPipelineName"
          v-model="pipelineName"
          type="text"
          placeholder="Enter pipeline name here"
          required
        />
      </b-form-group>
    </b-form>

    <b-row>
      <b-col cols="6">
        <h4>Available plugins</h4>
        <b-list-group id="availablePluginsList" v-if="getAllPlugins.length">
          <b-list-group-item
            button
            v-for="plugin in getAllPlugins"
            :key="plugin.id"
            @click="availablePluginListItemClicked(plugin)"
            >{{ plugin.name }} &gt;&gt;
          </b-list-group-item>
        </b-list-group>
        <b-alert variant="danger" :show="!getAllPlugins.length">
          No plugins available
        </b-alert>
      </b-col>

      <b-col cols="6">
        <h4>Selected plugins</h4>
        <b-list-group id="selectedPluginsList" v-if="selectedPlugins">
          <b-list-group-item
            button
            v-for="(plugin, index) in selectedPlugins"
            :key="index"
            >{{ plugin.name }} &gt;&gt;
          </b-list-group-item>
        </b-list-group>
        <b-alert variant="warning" :show="!selectedPlugins.length">
          No plugins selected
        </b-alert>
      </b-col>
    </b-row>
    <b-row>
      <b-col align-self="center">
        <b-button variant="primary" @click.prevent="saveButtonClicked()"
          >Save</b-button
        >&nbsp;
        <b-button variant="secondary" @click.prevent="goBackToPreviousPage()"
          >Cancel</b-button
        >
      </b-col>
    </b-row>
  </div>
</template>

<script>
import _ from "lodash";
import { mapActions, mapGetters } from "vuex";

export default {
  data() {
    return {
      pipelineName: "",
      selectedPlugins: []
    };
  },
  computed: {
    ...mapGetters(["getAllPlugins"]),
    op() {
      return _.capitalize(this.$route.params.operation);
    }
  },
  methods: {
    ...mapActions(["savePipeline"]),
    availablePluginListItemClicked(plugin) {
      var pluginCopy = { ...plugin };
      this.selectedPlugins.push(pluginCopy);
    },
    saveButtonClicked() {
      this.savePipeline({
        name: this.pipelineName,
        plugins: this.selectedPlugins
      });

      this.goBackToPreviousPage();
    },
    goBackToPreviousPage() {
      this.$router.go(-1);
    }
  }
};
</script>
