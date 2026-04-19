import { writeFileSync, existsSync, mkdirSync } from "fs";
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

// /* query keys */

// createFile(
//   `${basePath}/query-keys.ts`,
//   `export const ${feature.replace(/-/g, "_")}_keys = {
//   all: ["${feature}"],
//   list: ["${feature}", "list"],
//   detail: (id: string) => ["${feature}", id],
// }
// `,
// );

// /* api */

// createFile(
//   `${basePath}/api/${feature}.api.ts`,
//   `import { api } from "@/api/axios"

// export const get${toPascal(feature)} = async () => {
//   const res = await api.get("/${feature}")
//   return res.data
// }
// `,
// );

// /* query hook */

// createFile(
//   `${basePath}/hooks/queries/use-${feature}-query.ts`,
//   `import { useQuery } from "@tanstack/react-query"
// import { get${toPascal(feature)} } from "../../api/${feature}.api"
// import { ${feature.replace(/-/g, "_")}_keys } from "../../query-keys"

// export const use${toPascal(feature)}Query = () =>
//   useQuery({
//     queryKey: ${feature.replace(/-/g, "_")}_keys.list,
//     queryFn: get${toPascal(feature)},
//   })
// `,
// );

// /* mutation hook */

// createFile(
//   `${basePath}/hooks/mutations/use-${feature}-mutation.ts`,
//   `import { useMutation } from "@tanstack/react-query"

// export const use${toPascal(feature)}Mutation = () =>
//   useMutation({
//     mutationFn: async (data:any) => data
//   })
// `,
// );

// /* schema */

// createFile(
//   `${basePath}/schemas/${feature}.schema.ts`,
//   `import { z } from "zod"

// export const ${feature.replace(/-/g, "_")}_schema = z.object({
//   id: z.string()
// })
// `,
// );

// /* types */

// createFile(
//   `${basePath}/types/${feature}.types.ts`,
//   `export type ${toPascal(feature)} = {
//   id: string
// }
// `,
// );

// /* component */

// createFile(
//   `${basePath}/components/${feature}-component.tsx`,
//   `export function ${toPascal(feature)}Component() {
//   return <div>${feature} component</div>
// }
// `,
// );

/* index */

createFile(`${basePath}/index.ts`);

console.log(`✅ ${feature} feature created`);

function toPascal(str) {
  return str
    .split("-")
    .map((s) => s.charAt(0).toUpperCase() + s.slice(1))
    .join("");
}
