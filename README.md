---
title: README
created: 2026-31-01
updated: 2026-31-01
---

# Introduction

This project is a simple database of exercises that a user can navigate using a website. This was created by me to learn how to publish a web application using React and ASP.NET Core on Azure, and as an introduction to working with SQL Server. I have years of experience writing queries for SQL Server but have never administered or written to a SQL Server database. This also turned into my introduction to using Docker, since I needed a way to run a development database locally on my Mac.

The exercise data come from an open source json file in the [Free Exercise DB](https://github.com/yuhonas/free-exercise-db/tree/main) repository. I accessed this and created a copy of the json file on 2026-01-31. The repository also includes images, which I may include in my project in the future.

# Seeding Exercise Data

By default the App reads `exercises.json` on start up and inserts any that don't already exist in the database. This could be improved for the future to only run if it's in the Development environment. In production I think it would make sense to be able to add exercises using some sort of Admin panel, which could be used to seed the data on initial release.
