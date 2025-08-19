import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import { fileURLToPath, URL } from "node:url";

// Configuration
const API_PORT = 5001; // Should match the port where the API is running
const DEV_PORT = 3000;

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
  base: "/dist/", // Assets will be served from /dist/
  build: {
    outDir: "../wwwroot/dist", // Build to wwwroot/dist
    emptyOutDir: true,
    rollupOptions: {
      input: "./index.html",
      output: {
        entryFileNames: "js/[name].[hash].js",
        chunkFileNames: "js/[name].[hash].js",
        assetFileNames: "assets/[name].[hash].[ext]",
      },
    },
  },
  server: {
    port: DEV_PORT,
    proxy: {
      "/api": {
        target: `https://localhost:${API_PORT}`,
        changeOrigin: true,
        secure: false, // Set to false to ignore SSL certificate issues in development
      },
      "/swagger": {
        target: `https://localhost:${API_PORT}`,
        changeOrigin: true,
        secure: false,
      },
      "/openapi": {
        target: `https://localhost:${API_PORT}`,
        changeOrigin: true,
        secure: false,
      },
    },
  },
  publicDir: false, // Don't copy public assets automatically
});