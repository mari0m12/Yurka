# Yurka Design System

> **Yurka** is a gamified educational platform for **middle & high school students** that turns learning into an adventure. Interactive lessons become *quests*, progress becomes *XP, streaks and ranks*, and mastery becomes *achievements*. The identity is modern, fun and unmistakably **Gen-Alpha-friendly**: bright, rounded, tactile and energetic — learning that feels like playing.

This repository is the single source of truth for Yurka's brand: colors, type, fonts, assets, reusable React components, and full product UI kits.

---

## Source materials provided
- `COLOR PALETTE.docx` — the six brand colors (extracted into `tokens/colors.css`).
- `LOGO FOR CLAUDE.svg` — primary logo lockup (Deep-Sea bg, Sunshine mark, light wordmark). Derived assets in `assets/logo/`.
- `final characterpng.png` / `girl-mascotpng.png` — the two 3D mascots (`assets/mascots/`).
- `Untitled-1.png` — stacked logo + wordmark.
- `OMNES-ARABIC-*.ttf` (9 weights) — the Omnes UI/text typeface (`assets/fonts/`).
- `Chaloops Font.zip` — **NOT received / failed to upload.** Chaloops is the rounded display face used in the wordmark and big headlines. **Substituted with Baloo 2 (Google Fonts)** until the real files are supplied. See Caveats.

No codebase or Figma file was provided — components and UI kits are an original, faithful build from the brand assets and product description above.

---

## Content fundamentals (voice & tone)
Yurka talks like an **encouraging older friend / game guide** — never a stern teacher.

- **Person:** Second person, "you" / "your". Warm and direct: *"Ready for today's adventure?"*, *"You're 360 XP from Level 8."*
- **Tone:** Upbeat, motivating, playful but never childish or condescending. Celebrates effort (*"Not quite — keep going! 💪"*), not just correctness.
- **Casing:** Sentence case everywhere — headlines, buttons, labels. Reserve UPPERCASE only for tiny eyebrows/overlines (tracked `+0.08em`) and short flags like `NEW`.
- **Game vocabulary:** Borrow from games, not school. *Quest* (not assignment), *Challenge* (not test), *Streak*, *XP*, *Gems*, *Rank*, *Level up*, *Trophy case*, *Unlock*.
- **Brevity:** Punchy. Buttons are 1–3 words (*Start quest*, *Claim*, *Check answer*). Body copy is short and scannable.
- **Emoji & glyphs:** Used intentionally as friendly accents — 🔥 streaks, ⚡ XP, 🏅 ranks, 🎯 challenges, 💡 tips, 🎉 wins. One per element, never strings of them. They reinforce the gamified mood; keep them out of dense data tables.
- **Numbers:** Always frame progress as forward momentum ("18/25 lessons", "+50 XP"), never as a deficit or grade.

Example microcopy:
> Eyebrow: `DAILY QUEST` · H2: "Ready for today's adventure?" · Body: "Finish a quest to keep your 12-day streak alive!" · CTA: "Start daily quest →"

---

## Visual foundations
**Overall vibe:** bright, optimistic, rounded and tactile. Think a friendly mobile game crossed with a clean modern edu-app. High energy from saturated brand colors, grounded by Deep-Sea navy and generous white space.

- **Color:** Sunshine `#FFD60A` is the hero/primary (CTAs, XP, logo). Ocean Teal `#00B4DB` is the workhorse secondary (links, progress, the boy mascot). Electric Pink `#FF2E93` and Coral Pop `#FF6B47` are energetic accents (rank, streaks, highlights). Deep Sea `#0A2540` is the primary ink and the color for dark surfaces (sidebar, hero/CTA banners). Warm Gray `#F2F3F5` is the app canvas. Backgrounds are **flat color or very soft radial glows** — *avoid* heavy multi-stop gradients (one subtle Sunshine radial behind a mascot is the ceiling).
- **Type:** Two families. **Chaloops** (sub: Baloo 2) — rounded display — for headlines, numbers, buttons and anything that should feel playful. **Omnes** — geometric humanist sans — for body, UI labels and reading. Display is set tight (`-0.02em`, line-height ~1.05); body is comfortable (1.5).
- **Shape & radius:** Everything is **rounded and friendly**. Controls/buttons/badges are full pills (`--radius-pill`). Cards are 20px (`--radius-lg`); large panels 28px. Nothing sharp.
- **Cards:** White surface, 20px radius, hairline `--border-subtle` and a soft tinted shadow (`--shadow-sm`). Interactive cards lift `-3px` and grow to `--shadow-lg` on hover. Colored cards (teal/pink/sunshine/dark) drop the border.
- **Shadows:** Two systems. (1) **Soft elevation** — low-opacity Deep-Sea-tinted blurs (`--shadow-xs…xl`). (2) **Chunky game shadow** — a solid offset (e.g. `0 5px 0 var(--yk-sunshine-600)`) that makes buttons/cards read as 3D blocks; they **press down** on `:active` (translateY + shrink the offset). This tactile press is a signature interaction.
- **Borders:** Hairline `--border-subtle`/`--border-default` on neutral cards/inputs; 2px colored borders to show selection (teal) and quiz correctness (success/danger).
- **Motion:** Quick and bouncy. `--dur-fast 120ms` for presses, `--dur-base 200ms` for hovers, `--dur-slow 320ms` for progress fills. Easings: `--ease-out` for most, `--ease-bounce` (overshoot) for playful entrances. No long ambient/looping animations on content.
- **Hover state:** Cards lift + deepen shadow; nav items fill; buttons keep color (the affordance is the press, not hover).
- **Press state:** Solid buttons translate down 3px and the chunky shadow shrinks 5px→2px — a physical "click".
- **Focus:** 4px Teal-100 ring (`--focus-ring` / box-shadow), offset 2px. Always visible for accessibility.
- **Transparency & blur:** Sparingly — translucent white panels inside dark surfaces (sidebar user chip), and soft radial color glows behind mascots. No heavy glassmorphism.
- **Imagery:** The two **3D Pixar-style mascots** (a boy in a teal hoodie, a girl in a pink hoodie, both branded) are the primary imagery — warm, bright, friendly, full-body cut-outs with soft drop shadows, placed bleeding off the bottom of hero/CTA panels. Color vibe is warm and saturated, never muted, b&w, or grainy.
- **Layout:** Generous spacing on a 4px grid; max content width ~1200px. App uses a fixed 232px Deep-Sea sidebar + sticky HUD header. Marketing is centered, single-column sections with airy padding.

---

## Iconography
- **Primary approach today:** **emoji** are used as friendly, universally-recognized icons throughout the gamified UI — 🏠 home, 📚 lessons, 🎯 challenge, 🏅 ranks, 🔥 streak, ⚡ XP, ◆ gems, ★ rank, 💡 tips, 🎉/💪 feedback, and subject glyphs (📐 math, 🧪 science, 📖 English, 🏛️ history). This is intentional and on-brand for the Gen-Alpha audience; one glyph per element.
- **Unicode marks:** simple geometric glyphs (◆ ★ ✓ ✕ → ▶) are used for compact UI affordances.
- **No icon font or SVG icon set was provided.** If a line-icon system is later needed for denser/professional surfaces (e.g. school dashboards), the recommended substitute is **Lucide** (rounded, friendly, 2px stroke — matches the brand) via CDN. Flag any such addition. Do **not** hand-draw bespoke SVG icons.
- **Logo:** the Sunshine "Y/sprout" mark + Chaloops wordmark. Mark works solo as an app/favicon glyph. Assets in `assets/logo/` (transparent mark, transparent lockup, dark wordmark).

---

## Index / manifest
**Root**
- `styles.css` — global entry point (import-only). Consumers link this one file.
- `tokens/` — `fonts.css`, `colors.css`, `typography.css`, `spacing.css`, `base.css`.
- `assets/` — `fonts/` (Omnes ttf ×9), `logo/`, `mascots/`.
- `cards/` — foundation specimen cards (Colors, Type, Spacing, Brand) for the Design System tab.
- `readme.md` (this file) · `SKILL.md` (Agent-Skill manifest).

**Components** (`window.DesignSystem_663a1e.*`)
- `components/core/` — `Button`, `Badge`, `Card`, `Avatar`
- `components/game/` — `ProgressBar`, `StatPill`, `AchievementBadge`
- `components/forms/` — `Input`, `OptionCard`

**UI kits**
- `ui_kits/app/` — interactive learning app (Dashboard · Lesson · Challenge · Leaderboard).
- `ui_kits/marketing/` — landing page (hero · features · CTA · footer).

---

## Caveats
- **Chaloops font missing** — substituted with **Baloo 2** (Google Fonts), the closest rounded display match. Swap in `tokens/fonts.css` + `tokens/typography.css` once the real files arrive.
- **Mascot names** (Yuki / Mira) are *suggested* placeholders — confirm or replace.
- No codebase/Figma was supplied, so the UI kits are a faithful original interpretation of the brand, not a recreation of an existing build.
