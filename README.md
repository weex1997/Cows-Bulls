<!-- PROJECT LOGO -->
<p align="center">
  <img src="images/f11805456d9f3d08.png" width="200" height="200" alt="Game Logo"/>
</p>

<h2 align="center">Cows & Bulls - Number Guessing Game</h2>

<p align="center">
  🔗 <a href="https://www.youtube.com/watch?v=E1-fTTuxCIU">Game Overview</a> • 
  🔗 <a href="https://www.youtube.com/watch?v=WN8-BFGJ8NA">How to Create Lineups</a>
</p>

────────────────────────────────────

### 🧩 Introduction

*Cows and Bulls* is a logic-based number guessing game that I developed as a personal project, showcasing my skills in game development and design. The game offers a fun and intellectually stimulating experience.  

The idea was inspired by a friend who loved playing the original game and encouraged me to create a digital version. I added a unique twist to the result display using a virtual **invoice machine**, which prints out the results of each guess. This concept worked successfully and added a creative flair to the gameplay.

────────────────────────────────────

### ⚙️ Main Mechanics

- The game randomly generates a **4-digit number** using digits from 0 to 9, with **no repeating digits**.
- Players attempt to guess this number.
- After each guess:
  - A **bull** means a correct digit in the correct position.
  - A **cow** means a correct digit but in the wrong position.

The player uses these clues to narrow down the correct number through logic and deduction.

────────────────────────────────────

### ⭐ Features

#### 🏆 High Score System with PlayFab

<p align="center">
  <img src="images/Screenshot 2024-01-27 220106.png" width="300"/>
  <img src="images/Screenshot 2024-01-27 220254.png" width="300"/>
</p>

I integrated the **PlayFab API** to allow player names and high scores to be saved online.  
A custom service manager sends requests from Unity to the PlayFab server, storing player data and displaying a leaderboard.  
Players can also generate random names for a faster experience.

────────────────────────────────────

#### 📤 Share Results on Social Media

<p align="center">
  <img src="images/photo_2024-01-27_22-12-28.jpg" width="300"/>
</p>

To increase player engagement, I added a feature that allows players to **share their game results** on social media.  
I captured all guesses and progress in a single camera view and generated a combined screenshot. Players can then share this image with a custom message.

────────────────────────────────────

#### 🔍 Enhanced Guessing Aids

<p align="center">
  <img src="images/Screenshot 2024-01-29 110840.png" width="300"/>
</p>

To help players guess more effectively, I added interactive **analytics tools**:
- Players can **click numbers** to highlight or mark them.
- Two types of marks are supported: `"X"` for ruled-out digits and `"O"` for likely candidates.

────────────────────────────────────
