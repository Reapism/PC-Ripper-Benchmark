# PC-Ripper-Benchmark
The PC-Ripper-Benchmark application was made during our Senior project (BCS 430W) class. The
program benchmarks individual components on the PC and generates a score based
on an custom algorithm. Depending on the type of user you choose when creating
your account, the application will generate different results & information
to the user. The program features a login system that can store the account information
and results generated from the application. It has the ability to also query
the database for parts that are recommended for your PC.
The is an extension of my original work located here: https://github.com/Reapism/CPU-Ripper

## Authors:

> Reapism (Anthony J)
> dhartglass (David Hartglass)

> [See contributors page](https://github.com/Reapism/PC-Ripper-Benchmark/graphs/contributors)
---

# Development methodology
This project was developed in five sprints using the Agile methodology. 
Please follow the link below to view our trello board to see our user stories.

[![Click here to view the Agile Trello board](https://i0.wp.com/www.rxd.systems/wp-content/uploads/2016/05/LOGO-Trello.png?ssl=1)](https://trello.com/b/JQ7IIE37/tuesday-senior-project-sp-19-team-3)

The following video is a demo of the application.
--
[![Click here to go to the demo](https://img.youtube.com/vi/VrTnj4BXmwA/0.jpg)](https://www.youtube.com/watch?v=VrTnj4BXmwA)

# Running the setup
At the moment, the application installs to
`C:\Users\USER\AppData\Local\Apps\2.0`
if you run the installer.

Clone this repository to view the source.

# What is the PC-Ripper Benchmark application?
Let's call it Ripper for short. The Ripper is an app designed
to benchmark many components on your computer. It also has the
ability to query a database that has many components attributes.
We're able to ask pretty good questions through these queries so
it's always expandable. The program will generate a [score](https://github.com/Reapism/PC-Ripper-Benchmark/wiki/Score "Score - wiki")
contingent upon an internal algorithm that looks at how the
test was conducted, time it took to complete, and ticks per iteration.

# How can I download the application?
You can download this application by cloning the solution or by
downloading it using the green clone/download button on the top.

# Is the Ripper open-sourced?
Yes. All the code from the ground up is available for you
to see. We will likely be using other libraries within this project,
and depending on the license, we may or may not include that source-code
in this repository. 

# Is there more documentation for this project?
YES. Please check the WIP [Wiki](https://github.com/Reapism/PC-Ripper-Benchmark/wiki) tab!

# Credits
Thank you to `iconfinder.net` for the icons used in this project. The logo for PC Ripper Benchmark
was created by Reapism. The preloaders that show up during the test are viewed from an internal
browser. Non of the actual gif data is embedded in the program for copyright, just links to the
original sources to view. We do not own any of the icons (except the application logo), and 
preloaders.

# Packages used
`WpfAnimatedGif` `1.4.18`
https://github.com/XamlAnimatedGif/WpfAnimatedGif
Loaded in using NuGet.
