# Code Recipes


## Using docker

To build the CodeLiturgy.Views image use:

`docker build -f CodeLiturgy.Views/Dockerfile . -t myappdebug`

Copy the generated hash, and run:

` docker run -p 8080:80 image-hash`


## Using Docker Compose

Run the following command to instance a MySQL database and the API:

`docker-compose up --build -d`