#!/bin/bash
dotnet ef migrations add --project ZombieDAO/ZombieDAO.csproj --context DataContext "$1"