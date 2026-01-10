// Manga List AJAX Functions
// Helper function to get anti-forgery token

function getAntiForgeryToken() {
    return document.querySelector('input[name="__RequestVerificationToken"]').value;
}

// Helper function to show messages
function showMessage(message, isSuccess) {
    alert(message); 
}

// 1. Add manga to list
async function addToMangaList(mangaId, readStatus) {
    try {
        const response = await fetch('/MangaList/AddToList', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: new URLSearchParams({
                mangaId: mangaId,
                readStatus: readStatus
            })
        });

        const data = await response.json();

        if (data.success) {
            showMessage(data.message, true);
            return true;
        } else {
            showMessage(data.message, false);
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('An error occurred. Please try again.', false);
        return false;
    }
}

// 2. Update read status
async function updateMangaStatus(mangaId, readStatus) {
    try {
        const response = await fetch('/MangaList/UpdateStatus', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: new URLSearchParams({
                mangaId: mangaId,
                readStatus: readStatus
            })
        });

        const data = await response.json();

        if (data.success) {
            showMessage(data.message, true);
            return true;
        } else {
            showMessage(data.message, false);
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('An error occurred. Please try again.', false);
        return false;
    }
}
// 3. Update manga progress
async function updateMangaProgress(mangaId, progress) {
    try {
        const response = await fetch('/MangaList/UpdateProgress', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: new URLSearchParams({
                mangaId: mangaId,
                progress: progress
            })
        });

        const data = await response.json();

        if (data.success) {
            showMessage(data.message, true);
            return true;
        } else {
            showMessage(data.message, false);
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('An error occurred. Please try again.', false);
        return false;
    }
}
// 4. Update manga score
async function updateMangaScore(mangaId, score) {
    try {
        const response = await fetch('/MangaList/UpdateScore', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: new URLSearchParams({
                mangaId: mangaId,
                score: score
            })
        });

        const data = await response.json();

        if (data.success) {
            showMessage(data.message, true);
            return true;
        } else {
            showMessage(data.message, false);
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
    // ---------------------------------------------------------//

    //Manga Elements
    const addToMangaListBtn = document.getElementById('addToMangaList');
    const mangaStatusSelect = document.getElementById('mangaStatus');

    // Manga add
    if (addToMangaListBtn) {
        addToMangaListBtn.addEventListener('click', function (e) {
            e.preventDefault();
            const mangaId = this.dataset.mangaId;

            addToMangaList(mangaId, "Watching");

            addToMangaListBtn.style.display = 'none';
            mangaStatusSelect.style.display = 'block';
        });
    }

    // Update Manga Status dropdown
    if (mangaStatusSelect) {
        mangaStatusSelect.addEventListener('change', function () {
            const mangaId = this.dataset.mangaId;
            const readStatus = this.value;
            updateMangaStatus(mangaId, readStatus);
        });
    }

    // Update Manga Progress button 
    const mangaProgressInput = document.getElementById('mangaProgress');
    if (mangaProgressInput) {
        const mangaId = mangaProgressInput.dataset.mangaId;
        // Fires when user clicks away or presses Enter
        mangaProgressInput.addEventListener('blur', function () {
            const mangaProgress = parseInt(this.value);


            // Only update if there's a valid number
            if (!isNaN(mangaProgress) && mangaProgress >= 0) {
                updateMangaProgress(mangaId, mangaProgress);
            }
        });

        // Also fires when user presses Enter
        mangaProgressInput.addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                this.blur(); // Trigger the blur event
            }
        });
    }

    // Update Manga Score buttons
    const mangaScoreInput = document.getElementById('mangaScore');
    if (mangaScoreInput) {
        mangaScoreInput.addEventListener('change', function () {
            const mangaId = this.dataset.mangaId;
            updateMangaScore(mangaId, parseInt(this.value));
        });
    }

});
