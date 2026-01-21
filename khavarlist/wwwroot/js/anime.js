// Anime List AJAX Functions
// Helper function to get anti-forgery token
function getAntiForgeryToken() {
    return document.querySelector('input[name="__RequestVerificationToken"]').value;
}

// Helper function to show messages (you can customize this)
function showMessage(message, isSuccess) {
    alert(message); 
}

// 1. Add anime to list
async function addToList(animeId, watchStatus) {
    try {
        const response = await fetch('/AnimeList/AddToList', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: new URLSearchParams({
                animeId: animeId,
                watchStatus: watchStatus
            })
        });

        const data = await response.json();

        if (data.success) {
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('An error occurred. Please try again.', false);
        return false;
    }
}

// 2. Update watch status
async function updateStatus(animeId, watchStatus) {
    try {
        const response = await fetch('/AnimeList/UpdateStatus', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: new URLSearchParams({
                animeId: animeId,
                watchStatus: watchStatus
            })
        });

        const data = await response.json();

        if (data.success) {
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('An error occurred. Please try again.', false);
        return false;
    }
}

// 3. Update progress
async function updateProgress(animeId, progress, duration) {
    try {
        const response = await fetch('/AnimeList/UpdateProgress', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: new URLSearchParams({
                animeId: animeId,
                progress: progress,
                duration:duration
            })
        });

        const data = await response.json();

        if (data.success) {
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('An error occurred. Please try again.', false);
        return false;
    }
}

// 4. Update score
async function updateScore(animeId, score) {
    try {
        const response = await fetch('/AnimeList/UpdateScore', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: new URLSearchParams({
                animeId: animeId,
                score: score
            })
        });

        const data = await response.json();

        if (data.success) {
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('An error occurred. Please try again.', false);
        return false;
    }
}

// Initialize event listeners when DOM is loaded
document.addEventListener('DOMContentLoaded', function () {

    // Anime elements
    const addToListBtn = document.getElementById('addToListBtn');
    const statusSelect = document.getElementById('status');

    // Anime add
    if (addToListBtn) {
        addToListBtn.addEventListener('click', function (e) {
            e.preventDefault();
            const animeId = this.dataset.animeId;

            addToList(animeId, "Watching");

            addToListBtn.style.display = 'none';
            statusSelect.style.display = 'block';
        });
    }

    // Update Anime Status dropdown
    if (statusSelect) {
        statusSelect.addEventListener('change', function () {
            const animeId = this.dataset.animeId;
            const watchStatus = this.value;
            updateStatus(animeId, watchStatus);
        });
    }

    // Update Progress button
    const progressInput = document.getElementById('progress');
    
    if (progressInput) {
        const animeId = progressInput.dataset.animeId;
        const duration = progressInput.dataset.duration;
        if (animeId) {
            // Fires when user clicks away or presses Enter
            progressInput.addEventListener('blur', function () {
                const progress = parseInt(this.value);


                // Only update if there's a valid number
                if (!isNaN(progress) && progress >= 0) {
                    updateProgress(animeId, progress,duration);
                }
            });
        }

        // Also fires when user presses Enter
        progressInput.addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                this.blur(); // Trigger the blur event
            }
        });
    }

    // Update Score buttons
    const scoreInput = document.getElementById('score');
    if (scoreInput) {
        scoreInput.addEventListener('change', function () {
            const animeId = this.dataset.animeId;
            updateScore(animeId, parseInt(this.value));
        });
    }
});