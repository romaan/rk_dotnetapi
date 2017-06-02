docker build -t simpleapi .
docker run -d -p 5000:5000 --name simpleapi simpleapi
docker logs -f simpleapi
