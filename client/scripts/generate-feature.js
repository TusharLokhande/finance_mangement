import { existsSync, mkdirSync, writeFileSync } from "fs";
import { join } from "path";

const feature = process.argv[2];

if (!feature) {
  console.log("❌ Provide feature name");
  console.log("Example: npm run gen chat-room");
  process.exit(1);
}

const basePath = join("src", "features", feature);

const folders = [
  "api",
  "components",
  "hooks/queries",
  "hooks/mutations",
  "schemas",
  "types",
  "pages",
  "constants",
];

const createFile = (filePath, content = "") => {
  writeFileSync(filePath, content);
};

if (existsSync(basePath)) {
  console.log("⚠️ Feature already exists");
  process.exit(0);
}

folders.forEach((folder) => {
  mkdirSync(join(basePath, folder), { recursive: true });
});

createFile(`${basePath}/index.ts`);

console.log(`✅ ${feature} feature created`);

function toPascal(str) {
  return str
    .split("-")
    .map((s) => s.charAt(0).toUpperCase() + s.slice(1))
    .join("");
}
