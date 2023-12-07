// const PROXY_CONFIG = [
//   {
//     context: [
//       "/weatherforecast",
//       "/api/game/create",
//       "/api/game/get?{id}",
//       "/api/game/move?{id},{x},{y}"
//     ],
//     target: "http://localhost:5001",
//     secure: false
//   }
// ]

const PROXY_CONFIG = {
  "/api/*": {
      target: "http://127.0.0.1:5001",
      secure: false
  }
}

module.exports = PROXY_CONFIG;
