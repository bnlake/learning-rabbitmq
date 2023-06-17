FROM node:lts-alpine as build

RUN apk upgrade && apk update

WORKDIR /app

COPY ./package*.json ./
RUN npm ci

COPY ./ ./
RUN npm run build


FROM nginx:alpine as run

RUN apk upgrade && apk update

COPY --from=build /app/dist/ /usr/share/nginx/html

EXPOSE 80