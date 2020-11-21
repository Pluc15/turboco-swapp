/// <reference path="../references/signalr.min.js" />
/// <reference path="../references/vue.global.js" />

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/dashboard")
  .configureLogging(signalR.LogLevel.Information)
  .build();

const App = {
  data() {
    return {
      nbConnections: null,
      alerts: [],
      logs: [],
      newAlertMessage: null,
    };
  },
  methods: {
    OnAlert(message) {
      this.alerts.push(message);
    },
    OnConnectionCountChanged(count) {
      this.nbConnections = count;
    },
    async SendAlert() {
      try {
        const response = await fetch("api/alerts", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            message: this.newAlertMessage,
          }),
        });
        this.logs.push(response);
      } catch (err) {
        this.logs.push(err);
      }
    },
  },
  async mounted() {
    try {
      connection.on("OnAlert", this.OnAlert);
      connection.on("OnConnectionCountChanged", this.OnConnectionCountChanged);
      await connection.start();
      this.logs.push("SignalR Connected.");
    } catch (err) {
      this.logs.push(err);
    }
  },
};

Vue.createApp(App).mount("#app");
