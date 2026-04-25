FROM node:24-alpine AS build
WORKDIR /app

ARG VITE_API_BASE_URL
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL

# Install pnpm
RUN npm install -g pnpm

# Copy only files needed for install
COPY package.json pnpm-lock.yaml ./

# Install dependencies (including devDeps for the build)
RUN pnpm install --frozen-lockfile

# Copy the rest of the source code
COPY . .

# Run build via pnpm exec to ensure path resolution
RUN pnpm run build

# Final Stage
FROM nginx:alpine
# Remove default config
RUN rm /etc/nginx/conf.d/default.conf
# Copy your custom config (assuming it's named nginx.conf)
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html