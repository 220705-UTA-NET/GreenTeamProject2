# step 1
FROM node:16.16.0 as node
EXPOSE 80
WORKDIR /app
COPY ./ ./
RUN npm install
RUN npm run build --prod
# step 2
FROM nginx:alpine
COPY --from=node /app/dist/client /usr/share/nginx/html
