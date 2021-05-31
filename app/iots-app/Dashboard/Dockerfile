# pull official base image
FROM node:16.2.0

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY ./Dashboard/package.json ./
COPY ./Dashboard/package-lock.json ./
RUN npm install 
RUN npm install react-scripts -g

# add app
COPY ./Dashboard/ ./

# start app
CMD ["npm", "start"]
