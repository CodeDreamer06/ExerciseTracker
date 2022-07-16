# Exercise Tracker   ![GitHub commit merge status](https://img.shields.io/github/commit-status/CodeDreamer06/ExerciseTracker/main/c7607ac05699fb8a18efcded0d532822ab6d7c7a)&nbsp;  ![GitHub issues](https://img.shields.io/github/issues/Codedreamer06/ExerciseTracker)
A simple exercise logger for you to measure your progress!
## Getting Started:
Note: Use help to display this message
* **exit or 0**: stop the program
* **show [cardio or weights]**: display existing logs
* **add [cardio or weights]**: create an exercise log
* **update [id]**: change an existing log
* **remove [id]**: delete a log
## Main Program Flow UML
[![](https://mermaid.ink/img/pako:eNqNVF1v4jAQ_CuWn1KVwBXuaIsqpBOUE9J9VNDqXngx8ZJYdezU3tBGLf_9bAgpkML1KfF4d3a8nvUrjTQH2qORZNYOBYsNS2eKkPWa3BntAfLqIULO_2rzqHMcaIVGSwmGWDBLEUG5_4sJFZz5xWqm3mnqaVvC75xv4t3_NNHP1eIh4wyhWk4g1Uv4DPUDCmkPBU8g01agNgUZ16BNaPgDdrDAVL_3RQZnx9Omu2kjo9OxynIMHqF41oaX-kMXIlkEt2mGxUiA5DaQOj7byq8dq15ue6Sbm7FCMAvH1u-XpxQKiZM_0LnCqmUDA76DvkyJ_BQWyQTYe8vLKmvwwQoVT0AyFEsY80Dwg7vYYRqCBIeUIfsXAiJO0B7Vf377AlHukp9yMEU9fyThRcyFFFh8kqNBMuZM6gQZW-ebPjkqGM69Q-AFtxShRePOS0ZacjB3DJMDXEj47Uj3UUeiIEKh1XQNbApVzR7OD7p_z-YSgoQp7pzZIOiXnrUuc8AMF3rHzTtaz4dz57L9EFsK-6Nc8ELEuZcT6Mxrq7pA_Lc-fDdvzeZ2tD-M2MxQGNZ3fPiejp3LaTbf-kfm5KgpTuScNMKJvGPywrD_YZdP6vNJBw76r7YPc2iDpmBSJrh7bNf3OqOYgDMY7blfzszjjM7UysXl63G75Z6N9hZMWmhQlqOeFiqiPTQ5bIPKB7uKkpo5N9PeK0X3arlKsRt5RxmtPeLx3EgHJ4iZ7bVafrsZC0zyeTPSacsKnjCDyfK62-q2u1es3YHuZYd963R4NL-4vlq0v14s-OWXizajq9XqH0n2MQc)](https://mermaid.live/edit#pako:eNqNVF1v4jAQ_CuWn1KVwBXuaIsqpBOUE9J9VNDqXngx8ZJYdezU3tBGLf_9bAgpkML1KfF4d3a8nvUrjTQH2qORZNYOBYsNS2eKkPWa3BntAfLqIULO_2rzqHMcaIVGSwmGWDBLEUG5_4sJFZz5xWqm3mnqaVvC75xv4t3_NNHP1eIh4wyhWk4g1Uv4DPUDCmkPBU8g01agNgUZ16BNaPgDdrDAVL_3RQZnx9Omu2kjo9OxynIMHqF41oaX-kMXIlkEt2mGxUiA5DaQOj7byq8dq15ue6Sbm7FCMAvH1u-XpxQKiZM_0LnCqmUDA76DvkyJ_BQWyQTYe8vLKmvwwQoVT0AyFEsY80Dwg7vYYRqCBIeUIfsXAiJO0B7Vf377AlHukp9yMEU9fyThRcyFFFh8kqNBMuZM6gQZW-ebPjkqGM69Q-AFtxShRePOS0ZacjB3DJMDXEj47Uj3UUeiIEKh1XQNbApVzR7OD7p_z-YSgoQp7pzZIOiXnrUuc8AMF3rHzTtaz4dz57L9EFsK-6Nc8ELEuZcT6Mxrq7pA_Lc-fDdvzeZ2tD-M2MxQGNZ3fPiejp3LaTbf-kfm5KgpTuScNMKJvGPywrD_YZdP6vNJBw76r7YPc2iDpmBSJrh7bNf3OqOYgDMY7blfzszjjM7UysXl63G75Z6N9hZMWmhQlqOeFiqiPTQ5bIPKB7uKkpo5N9PeK0X3arlKsRt5RxmtPeLx3EgHJ4iZ7bVafrsZC0zyeTPSacsKnjCDyfK62-q2u1es3YHuZYd963R4NL-4vlq0v14s-OWXizajq9XqH0n2MQc)
## Utility & Model UML
[![](https://mermaid.ink/img/pako:eNqNU8Fy2jAQ_RWNTmQKoUBLEk97wrRlWuhMTKYXXxZrsTWRJVda03oI_16Z2CQxHHrzvn3e3fd2teeJEcgDnihwLpSQWshjzdgxZr-MfTQlsX0NMfZOamIL0QQhEK5ljiwisNQF57rl1WFUgGZhaYGk0Q3uyEqdspnJc9TkGrRtGSG1_N5VJ_fV5xCLmTIan5OHWL9MvSqV6ky-EOwzG4zOqA1tbcJNuP55WWcz52uVDfSiUZhyo_CkcKEjTIwW7r-kzozeofVDNEBPmbSRHNZl_N9lQheEfkNVoHWnsZs2S5B6ic5Bim8TK_PDpO5iqt3bF2Nz6MiMfquFdmjP4XsEcQY-FMIXO4NDVNjCg1AmtU9gKy8CBNpZBhYSQruEoj0l6QoF1Rq8sz3rjbDC9RnmBVWNhKuuvdbTKCpQKR_3Em83aHHu2_wvoXa-_xIpM8K92bs_rlWZb9D2OuXvMTc7_I7VHz_JaRuvj-3T02DQnhTv8xy9l1L413WsH3PKMMeYB_5TgH2MeawPnlceDZsLScbyYAvKYZ9DSSaqdMIDv31sSc0LPbGUqc3jwZ5TVdTvOJWubu2vZivTGi-t8nBGVLhgOKzT16mkrNxce3uGTgpvPGW7u-lwOp7ewniC05sJfJxMRLIZ3d1uxx9GW3HzfjQGfjgc_gHMXWgr)](https://mermaid.live/edit#pako:eNqNU8Fy2jAQ_RWNTmQKoUBLEk97wrRlWuhMTKYXXxZrsTWRJVda03oI_16Z2CQxHHrzvn3e3fd2teeJEcgDnihwLpSQWshjzdgxZr-MfTQlsX0NMfZOamIL0QQhEK5ljiwisNQF57rl1WFUgGZhaYGk0Q3uyEqdspnJc9TkGrRtGSG1_N5VJ_fV5xCLmTIan5OHWL9MvSqV6ky-EOwzG4zOqA1tbcJNuP55WWcz52uVDfSiUZhyo_CkcKEjTIwW7r-kzozeofVDNEBPmbSRHNZl_N9lQheEfkNVoHWnsZs2S5B6ic5Bim8TK_PDpO5iqt3bF2Nz6MiMfquFdmjP4XsEcQY-FMIXO4NDVNjCg1AmtU9gKy8CBNpZBhYSQruEoj0l6QoF1Rq8sz3rjbDC9RnmBVWNhKuuvdbTKCpQKR_3Em83aHHu2_wvoXa-_xIpM8K92bs_rlWZb9D2OuXvMTc7_I7VHz_JaRuvj-3T02DQnhTv8xy9l1L413WsH3PKMMeYB_5TgH2MeawPnlceDZsLScbyYAvKYZ9DSSaqdMIDv31sSc0LPbGUqc3jwZ5TVdTvOJWubu2vZivTGi-t8nBGVLhgOKzT16mkrNxce3uGTgpvPGW7u-lwOp7ewniC05sJfJxMRLIZ3d1uxx9GW3HzfjQGfjgc_gHMXWgr)
## Motivation & Features
This project is a simple command line based tool that helps you to keep track of your workouts. The purpose of this project is to gain an understanding of the repository pattern in C#. To further cement my comprehension using this pattern, I explored using two kinds of workouts - cardio and weight workouts. Cardio workouts are fetched using the Entity Framework and SQLite for weight workouts.

## Further reading & References
* EF Core docs: https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
* Implementing the repository pattern: https://www.programmingwithwolfgang.com/repository-pattern-net-core/
## Contribution
If you have any ideas,   [open an issue](https://github.com/CodeDreamer06/ExerciseTracker/issues/new)  and tell me what you think. If you'd like to contribute, please fork the repository and make changes as you'd like. Pull requests are warmly welcome.
1. Fork it
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new pull request