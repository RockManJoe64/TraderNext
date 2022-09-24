#!/bin/bash

# WARNING: If running from Windows, this wont' work in WSL at the moment.
# Run it from Git Bash

# Authenticate against AWS ECR Public Registry
aws ecr get-login-password --profile ecr --region us-east-2 | docker login --username AWS --password-stdin public.ecr.aws/z8u1m2l7

# Build and publish as linux-x64 compatible
dotnet publish -c Release -r linux-x64

# Create the docker image using tag tradernext
docker build -t tradernext .

# Tag the image
docker tag tradernext:latest public.ecr.aws/z8u1m2l7/tradernext:latest

# Push the image to ECR
docker push public.ecr.aws/z8u1m2l7/tradernext:latest

# Finally cleanup none tagged images
docker rmi $(docker images -f "dangling=true" -q)
