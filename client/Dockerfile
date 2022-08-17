# step 1
FROM node:latest as node
WORKDIR /usr/src/app
COPY . .
RUN npm install
RUN npm run build --prod
# step 2
FROM nginx:alpine
COPY --from=node usr/src/app/dist/client /usr/share/nginx/html
