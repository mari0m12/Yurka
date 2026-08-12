// ===== NAVBAR SCROLL =====
const navbar = document.getElementById('mainNav');
if (navbar) {
    window.addEventListener('scroll', () => {
        navbar.classList.toggle('scrolled', window.scrollY > 20);
    });
}

// ===== MOBILE MENU =====
const navToggle = document.getElementById('navToggle');
const mobileMenu = document.getElementById('mobileMenu');
if (navToggle && mobileMenu) {
    navToggle.addEventListener('click', () => {
        mobileMenu.classList.toggle('open');
        navToggle.classList.toggle('open');
    });
}

// ===== ALERT AUTO DISMISS =====
document.querySelectorAll('.alert-dismissible').forEach(alert => {
    setTimeout(() => {
        alert.style.opacity = '0';
        alert.style.transform = 'translateX(-50%) translateY(-20px)';
        setTimeout(() => alert.remove(), 400);
    }, 5000);

    alert.querySelector('.alert-close')?.addEventListener('click', () => {
        alert.style.opacity = '0';
        setTimeout(() => alert.remove(), 300);
    });
});

// ===== EXAM TIMER =====
function startExamTimer(endTimeStr) {
    const timerEl = document.getElementById('examTimer');
    if (!timerEl) return;

    const endTime = new Date(endTimeStr).getTime();

    const tick = () => {
        const now = Date.now();
        const diff = endTime - now;

        if (diff <= 0) {
            timerEl.textContent = '00:00';
            timerEl.closest('.exam-timer').style.background = '#dc2626';
            document.getElementById('examForm')?.submit();
            return;
        }

        const mins = Math.floor(diff / 60000);
        const secs = Math.floor((diff % 60000) / 1000);
        timerEl.textContent = `${String(mins).padStart(2, '0')}:${String(secs).padStart(2, '0')}`;

        if (diff < 300000) { // last 5 mins - warning
            timerEl.closest('.exam-timer').style.background = '#dc2626';
            timerEl.closest('.exam-timer').style.animation = 'pulse 1s infinite';
        }
    };

    tick();
    setInterval(tick, 1000);
}

// ===== EXAM ANSWER SELECT =====
document.querySelectorAll('.answer-option').forEach(option => {
    option.addEventListener('click', function () {
        const questionCard = this.closest('.question-card');
        questionCard.querySelectorAll('.answer-option').forEach(o => o.classList.remove('selected'));
        this.classList.add('selected');

        const radio = this.querySelector('input[type="radio"]');
        if (radio) radio.checked = true;

        questionCard.classList.add('answered');
        updateExamProgress();
    });
});

function updateExamProgress() {
    const total = document.querySelectorAll('.question-card').length;
    const answered = document.querySelectorAll('.question-card.answered').length;
    const pct = total > 0 ? (answered / total * 100) : 0;

    const fill = document.querySelector('.progress-bar-fill');
    const countEl = document.getElementById('answeredCount');
    if (fill) fill.style.width = pct + '%';
    if (countEl) countEl.textContent = answered;
}

// ===== GRADE TABS =====
document.querySelectorAll('.grade-tab').forEach(tab => {
    tab.addEventListener('click', function () {
        document.querySelectorAll('.grade-tab').forEach(t => t.classList.remove('active'));
        this.classList.add('active');

        const targetGrade = this.dataset.grade;
        document.querySelectorAll('.grade-section').forEach(section => {
            if (targetGrade === 'all' || section.dataset.grade === targetGrade) {
                section.style.display = '';
            } else {
                section.style.display = 'none';
            }
        });
    });
});

// ===== CONFIRM DIALOGS =====
document.querySelectorAll('[data-confirm]').forEach(btn => {
    btn.addEventListener('click', function (e) {
        if (!confirm(this.dataset.confirm)) e.preventDefault();
    });
});

// ===== SCROLL ANIMATIONS =====
const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.style.opacity = '1';
            entry.target.style.transform = 'translateY(0)';
        }
    });
}, { threshold: 0.1 });

document.querySelectorAll('.subject-card, .unit-card, .feature-card, .stat-card').forEach(el => {
    el.style.opacity = '0';
    el.style.transform = 'translateY(20px)';
    el.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
    observer.observe(el);
});

// ===== IMAGE PREVIEW =====
const receiptInput = document.getElementById('receiptInput');
const receiptPreview = document.getElementById('receiptPreview');
if (receiptInput && receiptPreview) {
    receiptInput.addEventListener('change', function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = e => {
                receiptPreview.src = e.target.result;
                receiptPreview.style.display = 'block';
            };
            reader.readAsDataURL(file);
        }
    });
}

// ===== ADD QUESTION FORM =====
const addAnswerBtn = document.getElementById('addAnswerBtn');
if (addAnswerBtn) {
    let answerCount = 4;
    addAnswerBtn.addEventListener('click', () => {
        const container = document.getElementById('answersContainer');
        const div = document.createElement('div');
        div.className = 'answer-input-row';
        div.innerHTML = `
            <input type="radio" name="correctAnswerIndex" value="${answerCount}" />
            <input type="text" name="answerTexts" class="form-control" placeholder="نص الإجابة ${answerCount + 1}" required />
        `;
        container.appendChild(div);
        answerCount++;
    });
}

// ===== COUNTER ANIMATION =====
function animateCounter(el, target) {
    let current = 0;
    const step = target / 60;
    const timer = setInterval(() => {
        current += step;
        if (current >= target) { current = target; clearInterval(timer); }
        el.textContent = Math.floor(current).toLocaleString('ar-EG');
    }, 16);
}

document.querySelectorAll('.hero-stat-num').forEach(el => {
    const target = parseInt(el.dataset.count || el.textContent.replace(/\D/g, ''));
    if (!isNaN(target)) animateCounter(el, target);
});
