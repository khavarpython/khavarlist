// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    // Find all scroll containers
    const scrollContainers = document.querySelectorAll('.scroll-container');

    scrollContainers.forEach(container => {
        const scrollContent = container.querySelector('.scroll-content');
        const leftBtn = container.querySelector('.scroll-arrow-left');
        const rightBtn = container.querySelector('.scroll-arrow-right');

        if (!scrollContent || !leftBtn || !rightBtn) return;

        function getScrollAmount() {
            const cardWidthRem = 11;
            const gapRem = 1;
            const remInPixels = parseFloat(getComputedStyle(document.documentElement).fontSize);
            return (cardWidthRem + gapRem) * 4 * remInPixels;
        }

        function updateArrows() {
            const isAtStart = scrollContent.scrollLeft <= 0;
            const isAtEnd = scrollContent.scrollLeft + scrollContent.clientWidth >= scrollContent.scrollWidth - 1;
            leftBtn.disabled = isAtStart;
            rightBtn.disabled = isAtEnd;
        }

        leftBtn.addEventListener('click', function () {
            scrollContent.scrollBy({
                left: -getScrollAmount(),
                behavior: 'smooth'
            });
        });

        rightBtn.addEventListener('click', function () {
            scrollContent.scrollBy({
                left: getScrollAmount(),
                behavior: 'smooth'
            });
        });

        scrollContent.addEventListener('scroll', updateArrows);
        updateArrows();
    });

    window.addEventListener('resize', function () {
        scrollContainers.forEach(container => {
            const scrollContent = container.querySelector('.scroll-content');
            const leftBtn = container.querySelector('.scroll-arrow-left');
            const rightBtn = container.querySelector('.scroll-arrow-right');

            if (scrollContent && leftBtn && rightBtn) {
                const isAtStart = scrollContent.scrollLeft <= 0;
                const isAtEnd = scrollContent.scrollLeft + scrollContent.clientWidth >= scrollContent.scrollWidth - 1;
                leftBtn.disabled = isAtStart;
                rightBtn.disabled = isAtEnd;
            }
        });
    });
});