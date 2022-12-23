#!/bin/bash
dotnet ef database update --project ZombieDAO/ZombieDAO.csproj --context DataContext "$1"