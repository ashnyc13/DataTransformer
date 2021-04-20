<template>
  <div>
    <h4>Manage selected pipeline</h4>
    <b-container>
      <b-row>
        <b-col lg="4" md="12" cols="4">
          <router-link to="/mp/add">
            <b-button>Add</b-button>
          </router-link>
        </b-col>
        <b-col lg="4" md="12" cols="4">
          <router-link :to="`/mp/edit/${selectedPipeline}`">
            <b-button>Edit</b-button>
          </router-link>
        </b-col>
        <b-col lg="4" md="12" cols="4"><b-button>Remove</b-button></b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
export default {
  props: {
    selectedPipeline: String,
  },
};
</script>
