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
      robotInfos: [],
      logs: [],
      newAlertMessage: "Some alert",
      newRobotInfo: {
        label: "Some robot",
        x: 1,
        y: 2,
        z: 3,
      },
    };
  },
  methods: {
    OnAlert(message, timestamp) {
      this.alerts.push({ message, timestamp });
    },
    OnRobotLocationChanged(label, x, y, z, timestamp) {
      let robot = this.robotInfos.find((robot) => robot.label == label);
      if (robot) {
        robot.x = x;
        robot.y = y;
        robot.z = z;
        robot.timestamp = timestamp;
      } else {
        this.robotInfos.push({ label, x, y, z, timestamp });
      }
    },
    OnConnectionCountChanged(count) {
      this.nbConnections = count;
    },
    ToggleDebug() {
      const debugDiv = document.getElementById("debug");
      if (debugDiv.style.display == "none") debugDiv.style.display = "block";
      else debugDiv.style.display = "none";
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
    async SendRobotInfo() {
      try {
        const response = await fetch(`api/robots/${this.newRobotInfo.label}`, {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            x: parseFloat(this.newRobotInfo.x),
            y: parseFloat(this.newRobotInfo.y),
            z: parseFloat(this.newRobotInfo.z),
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
      connection.on("OnRobotLocationChanged", this.OnRobotLocationChanged);
      connection.on("OnConnectionCountChanged", this.OnConnectionCountChanged);
      await connection.start();
      this.logs.push("SignalR Connected.");
    } catch (err) {
      this.logs.push(err);
    }
  },
};

Vue.createApp(App).mount("#app");
