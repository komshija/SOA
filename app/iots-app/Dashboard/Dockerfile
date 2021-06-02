FROM node:16.2.0 as build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY ./Dashboard/package.json ./
COPY ./Dashboard/package-lock.json ./
RUN npm ci --silent
RUN npm install react-scripts -g --silent
COPY ./Dashboard/ ./
RUN npm run build

FROM nginx:stable-alpine 
RUN rm /etc/nginx/conf.d/default.conf
COPY ./Dashboard/nginx-config/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD [ "nginx", "-g", "daemon off;" ]