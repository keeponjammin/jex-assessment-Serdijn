import { get } from "http";
import { execSync, spawn } from "child_process";

//Note: These are currently all the same but should/would be different in a production scenario.
const BACKEND_URL = process.env.BACKEND_URL || process.env.VITE_API_URL || "http://localhost:5001";

// Normally this would be more sophisticated
const checkBackendRunning = (url) => {
  return new Promise((resolve) => {
    const req = get(url, () => resolve(true));
    req.on("error", () => resolve(false));
    req.setTimeout(1000, () => {
      req.destroy();
      resolve(false);
    });
  });
};

(async () => {
  console.log(`Checking backend at: ${BACKEND_URL}`);
  const isBackendUp = await checkBackendRunning(BACKEND_URL);
  if (isBackendUp) {
    try {
      execSync("npm run generate-types", { stdio: "inherit" });
    } catch (e) {
      console.log("Type generation failed, continuing...");
    }
  } else {
    console.log("Backend not running, skipping type generation.");
  }
  // Start Vite dev server directly to avoid recursion
  spawn("npx", ["vite"], { stdio: "inherit", shell: true });
})();
