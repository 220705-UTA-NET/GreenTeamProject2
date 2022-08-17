# step 1
FROM node:latest as build
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build -- --prod
# step 2
FROM nginx:alpine
COPY --from=build /app/dist/client /usr/share/nginx/html
