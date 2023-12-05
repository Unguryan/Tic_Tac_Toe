const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "http://localhost:5001",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
