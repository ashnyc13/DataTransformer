export default {
  allPipelines: [
    {
      id: 1,
      name: "Ascii + Base64",
      plugins: [
        { id: 1, name: "Ascii" },
        { id: 3, name: "Base64" }
      ]
    },
    {
      id: 2,
      name: "Unicode + Base64",
      plugins: [
        { id: 2, name: "Unicode" },
        { id: 3, name: "Base64" }
      ]
    },
    { id: 3, name: "UrlEncode", plugins: [{ id: 4, name: "UrlEncode" }] }
  ],
  allPlugins: [
    { id: 1, name: "Ascii" },
    { id: 2, name: "Unicode" },
    { id: 3, name: "Base64" },
    { id: 4, name: "UrlEncode" }
  ]
};
