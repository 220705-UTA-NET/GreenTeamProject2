# step 1
FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install --force
RUN npm run build --prod
# step 2
FROM nginx:alpine
COPY --from=node /app/dist/client /usr/share/nginx/html
