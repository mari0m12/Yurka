/* @ds-bundle: {"format":3,"namespace":"DesignSystem_663a1e","components":[{"name":"Avatar","sourcePath":"components/core/Avatar.jsx"},{"name":"Badge","sourcePath":"components/core/Badge.jsx"},{"name":"Button","sourcePath":"components/core/Button.jsx"},{"name":"Card","sourcePath":"components/core/Card.jsx"},{"name":"Input","sourcePath":"components/forms/Input.jsx"},{"name":"OptionCard","sourcePath":"components/forms/OptionCard.jsx"},{"name":"AchievementBadge","sourcePath":"components/game/AchievementBadge.jsx"},{"name":"ProgressBar","sourcePath":"components/game/ProgressBar.jsx"},{"name":"StatPill","sourcePath":"components/game/StatPill.jsx"}],"sourceHashes":{"components/core/Avatar.jsx":"3e89cc378de3","components/core/Badge.jsx":"aa8858a6b063","components/core/Button.jsx":"49cadba411aa","components/core/Card.jsx":"a6237828cd0f","components/forms/Input.jsx":"e1babc29eea7","components/forms/OptionCard.jsx":"fd63ae421dc4","components/game/AchievementBadge.jsx":"45ee6927361a","components/game/ProgressBar.jsx":"2497c87e91aa","components/game/StatPill.jsx":"698ecd31176e","ui_kits/app/AppShell.jsx":"313ac4e47674","ui_kits/app/Dashboard.jsx":"b1af900af8b9","ui_kits/app/Leaderboard.jsx":"072ec08a920a","ui_kits/app/Lesson.jsx":"bd9a2b268669","ui_kits/app/Quiz.jsx":"38f1a977730d","ui_kits/app/data.js":"cb858bbf660b","ui_kits/marketing/Sections.jsx":"9f7eadbefc8d"},"inlinedExternals":[],"unexposedExports":[]} */

(() => {

const __ds_ns = (window.DesignSystem_663a1e = window.DesignSystem_663a1e || {});

const __ds_scope = {};

(__ds_ns.__errors = __ds_ns.__errors || []);

// components/core/Avatar.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * Avatar — round student avatar with optional colored ring (used to
 * indicate rank tier or active streak) and online dot.
 */
function Avatar({
  src = null,
  name = '',
  size = 44,
  ring = null,
  // null | 'sunshine' | 'teal' | 'pink' | 'coral'
  online = false,
  style = {},
  ...rest
}) {
  const rings = {
    sunshine: 'var(--yk-sunshine)',
    teal: 'var(--yk-teal)',
    pink: 'var(--yk-pink)',
    coral: 'var(--yk-coral)'
  };
  const initials = (name || '?').split(' ').map(w => w[0]).slice(0, 2).join('').toUpperCase();
  const ringColor = ring ? rings[ring] : null;
  return /*#__PURE__*/React.createElement("div", _extends({
    style: {
      width: size,
      height: size,
      borderRadius: '50%',
      background: src ? `center/cover url(${src})` : 'var(--yk-teal-100)',
      color: 'var(--yk-teal-700)',
      display: 'inline-flex',
      alignItems: 'center',
      justifyContent: 'center',
      fontFamily: 'var(--font-display)',
      fontWeight: 700,
      fontSize: size * 0.4,
      position: 'relative',
      flex: 'none',
      boxShadow: ringColor ? `0 0 0 3px var(--surface-card), 0 0 0 6px ${ringColor}` : 'none',
      ...style
    }
  }, rest), !src && initials, online && /*#__PURE__*/React.createElement("span", {
    style: {
      position: 'absolute',
      bottom: 0,
      right: 0,
      width: size * 0.28,
      height: size * 0.28,
      borderRadius: '50%',
      background: 'var(--yk-success)',
      border: '2px solid var(--surface-card)'
    }
  }));
}
Object.assign(__ds_scope, { Avatar });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Avatar.jsx", error: String((e && e.message) || e) }); }

// components/core/Badge.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * Badge — small pill label for status, categories, counts and "NEW" flags.
 */
function Badge({
  children,
  color = 'teal',
  soft = false,
  size = 'md',
  icon = null,
  style = {},
  ...rest
}) {
  const map = {
    sunshine: {
      solid: 'var(--yk-sunshine)',
      sfg: 'var(--yk-deepsea)',
      soft: 'var(--yk-sunshine-100)',
      sft: 'var(--yk-sunshine-700)'
    },
    teal: {
      solid: 'var(--yk-teal)',
      sfg: '#fff',
      soft: 'var(--yk-teal-50)',
      sft: 'var(--yk-teal-700)'
    },
    pink: {
      solid: 'var(--yk-pink)',
      sfg: '#fff',
      soft: 'var(--yk-pink-50)',
      sft: 'var(--yk-pink-600)'
    },
    coral: {
      solid: 'var(--yk-coral)',
      sfg: '#fff',
      soft: 'var(--yk-coral-50)',
      sft: 'var(--yk-coral-600)'
    },
    success: {
      solid: 'var(--yk-success)',
      sfg: '#fff',
      soft: 'var(--yk-success-soft)',
      sft: 'var(--yk-success)'
    },
    dark: {
      solid: 'var(--yk-deepsea)',
      sfg: '#fff',
      soft: 'var(--yk-ink-100)',
      sft: 'var(--yk-ink-800)'
    }
  };
  const c = map[color] || map.teal;
  const sizes = {
    sm: {
      p: '3px 9px',
      fs: '11px'
    },
    md: {
      p: '5px 12px',
      fs: '13px'
    },
    lg: {
      p: '7px 16px',
      fs: '14px'
    }
  };
  const s = sizes[size] || sizes.md;
  return /*#__PURE__*/React.createElement("span", _extends({
    style: {
      display: 'inline-flex',
      alignItems: 'center',
      gap: '5px',
      fontFamily: 'var(--font-sans)',
      fontWeight: 700,
      fontSize: s.fs,
      padding: s.p,
      borderRadius: 'var(--radius-pill)',
      lineHeight: 1,
      background: soft ? c.soft : c.solid,
      color: soft ? c.sft : c.sfg,
      ...style
    }
  }, rest), icon, children);
}
Object.assign(__ds_scope, { Badge });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Badge.jsx", error: String((e && e.message) || e) }); }

// components/core/Button.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * Yurka Button — playful, chunky, pill-shaped call to action.
 * Variants map to brand colors; the "chunk" shadow gives a tactile
 * game-button feel that presses down on :active.
 */
function Button({
  children,
  variant = 'primary',
  size = 'md',
  block = false,
  disabled = false,
  iconLeft = null,
  iconRight = null,
  type = 'button',
  onClick,
  style = {},
  ...rest
}) {
  const palette = {
    primary: {
      bg: 'var(--yk-sunshine)',
      fg: 'var(--yk-deepsea)',
      chunk: 'var(--yk-sunshine-600)'
    },
    teal: {
      bg: 'var(--yk-teal)',
      fg: '#fff',
      chunk: 'var(--yk-teal-600)'
    },
    pink: {
      bg: 'var(--yk-pink)',
      fg: '#fff',
      chunk: 'var(--yk-pink-600)'
    },
    coral: {
      bg: 'var(--yk-coral)',
      fg: '#fff',
      chunk: 'var(--yk-coral-600)'
    },
    dark: {
      bg: 'var(--yk-deepsea)',
      fg: '#fff',
      chunk: '#04101d'
    }
  };
  const sizes = {
    sm: {
      pad: '8px 16px',
      fs: '14px',
      gap: '6px'
    },
    md: {
      pad: '12px 24px',
      fs: '16px',
      gap: '8px'
    },
    lg: {
      pad: '16px 32px',
      fs: '18px',
      gap: '10px'
    }
  };
  const isGhost = variant === 'ghost';
  const isOutline = variant === 'outline';
  const p = palette[variant] || palette.primary;
  const s = sizes[size] || sizes.md;
  const base = {
    fontFamily: 'var(--font-display)',
    fontWeight: 700,
    fontSize: s.fs,
    padding: s.pad,
    display: block ? 'flex' : 'inline-flex',
    width: block ? '100%' : undefined,
    alignItems: 'center',
    justifyContent: 'center',
    gap: s.gap,
    border: 'none',
    borderRadius: 'var(--radius-pill)',
    cursor: disabled ? 'not-allowed' : 'pointer',
    opacity: disabled ? 0.5 : 1,
    transition: 'transform var(--dur-fast) var(--ease-out), box-shadow var(--dur-fast) var(--ease-out), background var(--dur-fast) var(--ease-out)',
    lineHeight: 1.1,
    whiteSpace: 'nowrap',
    userSelect: 'none'
  };
  let variantStyle;
  if (isGhost) {
    variantStyle = {
      background: 'transparent',
      color: 'var(--yk-teal-700)'
    };
  } else if (isOutline) {
    variantStyle = {
      background: '#fff',
      color: 'var(--text-strong)',
      boxShadow: 'inset 0 0 0 2px var(--border-default)'
    };
  } else {
    variantStyle = {
      background: p.bg,
      color: p.fg,
      boxShadow: `0 5px 0 ${p.chunk}`
    };
  }
  const press = (e, down) => {
    if (disabled || isGhost || isOutline) return;
    e.currentTarget.style.transform = down ? 'translateY(3px)' : 'translateY(0)';
    e.currentTarget.style.boxShadow = down ? `0 2px 0 ${p.chunk}` : `0 5px 0 ${p.chunk}`;
  };
  return /*#__PURE__*/React.createElement("button", _extends({
    type: type,
    disabled: disabled,
    onClick: onClick,
    onMouseDown: e => press(e, true),
    onMouseUp: e => press(e, false),
    onMouseLeave: e => press(e, false),
    style: {
      ...base,
      ...variantStyle,
      ...style
    }
  }, rest), iconLeft, children, iconRight);
}
Object.assign(__ds_scope, { Button });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Button.jsx", error: String((e && e.message) || e) }); }

// components/core/Card.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * Card — the universal rounded surface. White by default; supports a
 * colored "accent" top, dark theme, and an optional chunky 3D shadow
 * for interactive/game cards.
 */
function Card({
  children,
  tone = 'plain',
  // plain | dark | sunshine | teal | pink | coral
  interactive = false,
  chunk = false,
  pad = 'md',
  // none | sm | md | lg
  style = {},
  ...rest
}) {
  const tones = {
    plain: {
      bg: 'var(--surface-card)',
      fg: 'var(--text-body)',
      border: '1px solid var(--border-subtle)'
    },
    dark: {
      bg: 'var(--yk-deepsea)',
      fg: '#fff',
      border: 'none'
    },
    sunshine: {
      bg: 'var(--yk-sunshine)',
      fg: 'var(--yk-deepsea)',
      border: 'none'
    },
    teal: {
      bg: 'var(--yk-teal)',
      fg: '#fff',
      border: 'none'
    },
    pink: {
      bg: 'var(--yk-pink)',
      fg: '#fff',
      border: 'none'
    },
    coral: {
      bg: 'var(--yk-coral)',
      fg: '#fff',
      border: 'none'
    },
    softteal: {
      bg: 'var(--yk-teal-50)',
      fg: 'var(--text-body)',
      border: '1px solid var(--yk-teal-100)'
    }
  };
  const pads = {
    none: '0',
    sm: '16px',
    md: '22px',
    lg: '28px'
  };
  const t = tones[tone] || tones.plain;
  const [hover, setHover] = React.useState(false);
  const base = {
    borderRadius: 'var(--radius-lg)',
    background: t.bg,
    color: t.fg,
    border: t.border,
    padding: pads[pad] ?? pads.md,
    boxShadow: chunk ? 'var(--shadow-chunk-sm)' : 'var(--shadow-sm)',
    transition: 'transform var(--dur-base) var(--ease-out), box-shadow var(--dur-base) var(--ease-out)',
    cursor: interactive ? 'pointer' : 'default',
    transform: interactive && hover ? 'translateY(-3px)' : 'none',
    ...(interactive && hover ? {
      boxShadow: 'var(--shadow-lg)'
    } : {})
  };
  return /*#__PURE__*/React.createElement("div", _extends({
    onMouseEnter: () => interactive && setHover(true),
    onMouseLeave: () => interactive && setHover(false),
    style: {
      ...base,
      ...style
    }
  }, rest), children);
}
Object.assign(__ds_scope, { Card });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Card.jsx", error: String((e && e.message) || e) }); }

// components/forms/Input.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * Input — rounded text field with label, optional leading icon and helper/
 * error text. Focus shows the teal ring.
 */
function Input({
  label = null,
  hint = null,
  error = null,
  iconLeft = null,
  size = 'md',
  style = {},
  id,
  ...rest
}) {
  const sizes = {
    md: {
      p: '12px 16px',
      fs: 16
    },
    lg: {
      p: '15px 18px',
      fs: 17
    }
  };
  const s = sizes[size] || sizes.md;
  const [focus, setFocus] = React.useState(false);
  const inputId = id || `yk-${Math.random().toString(36).slice(2, 8)}`;
  const borderColor = error ? 'var(--yk-danger)' : focus ? 'var(--yk-teal)' : 'var(--border-default)';
  return /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      flexDirection: 'column',
      gap: 6,
      ...style
    }
  }, label && /*#__PURE__*/React.createElement("label", {
    htmlFor: inputId,
    style: {
      fontSize: 14,
      fontWeight: 600,
      color: 'var(--text-strong)'
    }
  }, label), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 10,
      background: 'var(--surface-card)',
      borderRadius: 'var(--radius-md)',
      border: `2px solid ${borderColor}`,
      boxShadow: focus ? '0 0 0 4px var(--yk-teal-100)' : 'none',
      padding: s.p,
      transition: 'border-color var(--dur-fast), box-shadow var(--dur-fast)'
    }
  }, iconLeft && /*#__PURE__*/React.createElement("span", {
    style: {
      color: 'var(--text-muted)',
      display: 'flex'
    }
  }, iconLeft), /*#__PURE__*/React.createElement("input", _extends({
    id: inputId,
    onFocus: () => setFocus(true),
    onBlur: () => setFocus(false),
    style: {
      border: 'none',
      outline: 'none',
      flex: 1,
      background: 'transparent',
      fontFamily: 'var(--font-sans)',
      fontSize: s.fs,
      color: 'var(--text-strong)',
      minWidth: 0
    }
  }, rest))), (hint || error) && /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 13,
      color: error ? 'var(--yk-danger)' : 'var(--text-muted)'
    }
  }, error || hint));
}
Object.assign(__ds_scope, { Input });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/forms/Input.jsx", error: String((e && e.message) || e) }); }

// components/forms/OptionCard.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * OptionCard — a selectable answer tile for quizzes/challenges. Shows
 * neutral, selected, correct and wrong states with a leading key badge.
 */
function OptionCard({
  children,
  letter = 'A',
  state = 'default',
  // default | selected | correct | wrong
  onClick,
  disabled = false,
  style = {},
  ...rest
}) {
  const states = {
    default: {
      border: 'var(--border-default)',
      bg: 'var(--surface-card)',
      badge: 'var(--yk-ink-100)',
      badgeFg: 'var(--text-strong)',
      fg: 'var(--text-strong)'
    },
    selected: {
      border: 'var(--yk-teal)',
      bg: 'var(--yk-teal-50)',
      badge: 'var(--yk-teal)',
      badgeFg: '#fff',
      fg: 'var(--yk-teal-700)'
    },
    correct: {
      border: 'var(--yk-success)',
      bg: 'var(--yk-success-soft)',
      badge: 'var(--yk-success)',
      badgeFg: '#fff',
      fg: 'var(--yk-success)'
    },
    wrong: {
      border: 'var(--yk-danger)',
      bg: 'var(--yk-danger-soft)',
      badge: 'var(--yk-danger)',
      badgeFg: '#fff',
      fg: 'var(--yk-danger)'
    }
  };
  const st = states[state] || states.default;
  const [hover, setHover] = React.useState(false);
  return /*#__PURE__*/React.createElement("button", _extends({
    type: "button",
    onClick: onClick,
    disabled: disabled,
    onMouseEnter: () => setHover(true),
    onMouseLeave: () => setHover(false),
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 14,
      width: '100%',
      textAlign: 'left',
      background: st.bg,
      border: `2px solid ${st.border}`,
      borderRadius: 'var(--radius-md)',
      padding: '14px 18px',
      cursor: disabled ? 'default' : 'pointer',
      fontFamily: 'var(--font-sans)',
      fontSize: 16,
      fontWeight: 600,
      color: st.fg,
      boxShadow: state === 'default' && hover && !disabled ? 'var(--shadow-sm)' : 'none',
      transform: state === 'default' && hover && !disabled ? 'translateY(-1px)' : 'none',
      transition: 'all var(--dur-fast) var(--ease-out)',
      ...style
    }
  }, rest), /*#__PURE__*/React.createElement("span", {
    style: {
      width: 32,
      height: 32,
      flex: 'none',
      borderRadius: 'var(--radius-sm)',
      background: st.badge,
      color: st.badgeFg,
      display: 'inline-flex',
      alignItems: 'center',
      justifyContent: 'center',
      fontFamily: 'var(--font-display)',
      fontWeight: 800,
      fontSize: 16
    }
  }, letter), /*#__PURE__*/React.createElement("span", {
    style: {
      flex: 1
    }
  }, children), state === 'correct' && /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 20
    }
  }, "\u2713"), state === 'wrong' && /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 20
    }
  }, "\u2715"));
}
Object.assign(__ds_scope, { OptionCard });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/forms/OptionCard.jsx", error: String((e && e.message) || e) }); }

// components/game/AchievementBadge.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * AchievementBadge — circular achievement medal with glyph, used in the
 * trophy case and reward popups. Locked state desaturates.
 */
function AchievementBadge({
  glyph = '★',
  title = '',
  color = 'sunshine',
  // sunshine | teal | pink | coral
  locked = false,
  size = 76,
  style = {},
  ...rest
}) {
  const colors = {
    sunshine: 'var(--yk-sunshine)',
    teal: 'var(--yk-teal)',
    pink: 'var(--yk-pink)',
    coral: 'var(--yk-coral)'
  };
  const c = colors[color] || colors.sunshine;
  return /*#__PURE__*/React.createElement("div", _extends({
    style: {
      display: 'inline-flex',
      flexDirection: 'column',
      alignItems: 'center',
      gap: 8,
      width: size + 24,
      ...style
    }
  }, rest), /*#__PURE__*/React.createElement("div", {
    style: {
      width: size,
      height: size,
      borderRadius: '50%',
      background: locked ? 'var(--yk-ink-100)' : c,
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      fontSize: size * 0.42,
      color: locked ? 'var(--yk-ink-400)' : color === 'sunshine' ? 'var(--yk-deepsea)' : '#fff',
      boxShadow: locked ? 'none' : '0 6px 0 rgba(10,37,64,0.14), inset 0 -4px 8px rgba(0,0,0,0.12)',
      border: locked ? '2px dashed var(--border-default)' : '3px solid rgba(255,255,255,0.6)',
      filter: locked ? 'grayscale(1)' : 'none'
    }
  }, locked ? '🔒' : glyph), title && /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 12,
      fontWeight: 700,
      textAlign: 'center',
      color: locked ? 'var(--text-faint)' : 'var(--text-strong)',
      fontFamily: 'var(--font-sans)'
    }
  }, title));
}
Object.assign(__ds_scope, { AchievementBadge });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/game/AchievementBadge.jsx", error: String((e && e.message) || e) }); }

// components/game/ProgressBar.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * ProgressBar — rounded XP / lesson progress track. Optional label and
 * value, brand-colored fill, and a soft track.
 */
function ProgressBar({
  value = 0,
  max = 100,
  color = 'sunshine',
  // sunshine | teal | pink | coral | success
  height = 14,
  label = null,
  showValue = false,
  style = {},
  ...rest
}) {
  const fills = {
    sunshine: 'var(--yk-sunshine)',
    teal: 'var(--yk-teal)',
    pink: 'var(--yk-pink)',
    coral: 'var(--yk-coral)',
    success: 'var(--yk-success)'
  };
  const pct = Math.max(0, Math.min(100, value / max * 100));
  return /*#__PURE__*/React.createElement("div", _extends({
    style: {
      ...style
    }
  }, rest), (label || showValue) && /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      justifyContent: 'space-between',
      marginBottom: 6,
      fontSize: 13,
      fontWeight: 600,
      color: 'var(--text-muted)'
    }
  }, /*#__PURE__*/React.createElement("span", null, label), showValue && /*#__PURE__*/React.createElement("span", {
    style: {
      color: 'var(--text-strong)'
    }
  }, value, "/", max)), /*#__PURE__*/React.createElement("div", {
    style: {
      background: 'var(--yk-ink-100)',
      borderRadius: 'var(--radius-pill)',
      height,
      overflow: 'hidden'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      width: `${pct}%`,
      height: '100%',
      background: fills[color] || fills.sunshine,
      borderRadius: 'var(--radius-pill)',
      transition: 'width var(--dur-slow) var(--ease-out)'
    }
  })));
}
Object.assign(__ds_scope, { ProgressBar });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/game/ProgressBar.jsx", error: String((e && e.message) || e) }); }

// components/game/StatPill.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/**
 * StatPill — compact gamification stat (XP, streak, gems, rank). A rounded
 * chip with a glyph in a tinted circle and a bold value.
 */
function StatPill({
  kind = 'xp',
  // xp | streak | gems | rank | custom
  value,
  label = null,
  glyph = null,
  style = {},
  ...rest
}) {
  const presets = {
    xp: {
      glyph: '⚡',
      color: 'var(--yk-sunshine)',
      ink: 'var(--yk-deepsea)',
      tint: 'var(--yk-sunshine-100)'
    },
    streak: {
      glyph: '🔥',
      color: 'var(--yk-coral)',
      ink: '#fff',
      tint: 'var(--yk-coral-100)'
    },
    gems: {
      glyph: '◆',
      color: 'var(--yk-teal)',
      ink: '#fff',
      tint: 'var(--yk-teal-100)'
    },
    rank: {
      glyph: '★',
      color: 'var(--yk-pink)',
      ink: '#fff',
      tint: 'var(--yk-pink-100)'
    },
    custom: {
      glyph: glyph || '•',
      color: 'var(--yk-ink-800)',
      ink: '#fff',
      tint: 'var(--yk-ink-100)'
    }
  };
  const p = presets[kind] || presets.custom;
  return /*#__PURE__*/React.createElement("div", _extends({
    style: {
      display: 'inline-flex',
      alignItems: 'center',
      gap: 9,
      background: 'var(--surface-card)',
      border: '1px solid var(--border-subtle)',
      borderRadius: 'var(--radius-pill)',
      padding: '6px 14px 6px 6px',
      boxShadow: 'var(--shadow-xs)',
      ...style
    }
  }, rest), /*#__PURE__*/React.createElement("span", {
    style: {
      width: 30,
      height: 30,
      borderRadius: '50%',
      background: p.tint,
      display: 'inline-flex',
      alignItems: 'center',
      justifyContent: 'center',
      fontSize: 15
    }
  }, glyph || p.glyph), /*#__PURE__*/React.createElement("span", {
    style: {
      display: 'flex',
      flexDirection: 'column',
      lineHeight: 1.05
    }
  }, /*#__PURE__*/React.createElement("span", {
    style: {
      fontFamily: 'var(--font-display)',
      fontWeight: 800,
      fontSize: 17,
      color: 'var(--text-strong)'
    }
  }, value), label && /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 11,
      fontWeight: 600,
      color: 'var(--text-muted)'
    }
  }, label)));
}
Object.assign(__ds_scope, { StatPill });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/game/StatPill.jsx", error: String((e && e.message) || e) }); }

// ui_kits/app/AppShell.jsx
try { (() => {
// AppShell — sidebar nav + top HUD bar. Exposes window.YK_AppShell
function YK_AppShell({
  active,
  onNav,
  children
}) {
  const {
    StatPill,
    Avatar
  } = window.DesignSystem_663a1e;
  const u = window.YK_DATA.user;
  const nav = [{
    id: 'dashboard',
    label: 'Home',
    glyph: '🏠'
  }, {
    id: 'lesson',
    label: 'Lessons',
    glyph: '📚'
  }, {
    id: 'quiz',
    label: 'Challenge',
    glyph: '🎯'
  }, {
    id: 'leaderboard',
    label: 'Ranks',
    glyph: '🏅'
  }];
  return /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      minHeight: '100%',
      background: 'var(--surface-page)'
    }
  }, /*#__PURE__*/React.createElement("aside", {
    style: {
      width: 232,
      flex: 'none',
      background: 'var(--yk-deepsea)',
      color: '#fff',
      padding: '24px 18px',
      display: 'flex',
      flexDirection: 'column',
      gap: 8,
      position: 'sticky',
      top: 0,
      height: '100vh'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 10,
      padding: '0 8px 22px'
    }
  }, /*#__PURE__*/React.createElement("img", {
    src: "../../assets/logo/yurka-mark.svg",
    alt: "",
    style: {
      height: 34
    }
  }), /*#__PURE__*/React.createElement("img", {
    src: "../../assets/logo/yurka-wordmark-dark.svg",
    alt: "Yurka",
    style: {
      height: 22,
      filter: 'brightness(0) invert(1)'
    }
  })), nav.map(n => {
    const on = active === n.id;
    return /*#__PURE__*/React.createElement("button", {
      key: n.id,
      onClick: () => onNav(n.id),
      style: {
        display: 'flex',
        alignItems: 'center',
        gap: 12,
        width: '100%',
        textAlign: 'left',
        padding: '12px 14px',
        borderRadius: 'var(--radius-md)',
        border: 'none',
        cursor: 'pointer',
        background: on ? 'var(--yk-sunshine)' : 'transparent',
        color: on ? 'var(--yk-deepsea)' : 'rgba(255,255,255,0.78)',
        fontFamily: 'var(--font-display)',
        fontWeight: 700,
        fontSize: 16,
        transition: 'background var(--dur-fast)'
      }
    }, /*#__PURE__*/React.createElement("span", {
      style: {
        fontSize: 18
      }
    }, n.glyph), n.label);
  }), /*#__PURE__*/React.createElement("div", {
    style: {
      marginTop: 'auto',
      display: 'flex',
      alignItems: 'center',
      gap: 10,
      padding: '12px 8px',
      background: 'rgba(255,255,255,0.06)',
      borderRadius: 'var(--radius-md)'
    }
  }, /*#__PURE__*/React.createElement(Avatar, {
    name: u.name,
    ring: "pink",
    size: 40
  }), /*#__PURE__*/React.createElement("div", {
    style: {
      lineHeight: 1.15
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      fontWeight: 700,
      fontSize: 14
    }
  }, u.name.split(' ')[0]), /*#__PURE__*/React.createElement("div", {
    style: {
      fontSize: 12,
      color: 'rgba(255,255,255,0.6)'
    }
  }, "Level ", u.level)))), /*#__PURE__*/React.createElement("div", {
    style: {
      flex: 1,
      minWidth: 0,
      display: 'flex',
      flexDirection: 'column'
    }
  }, /*#__PURE__*/React.createElement("header", {
    style: {
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      padding: '18px 32px',
      background: 'var(--surface-card)',
      borderBottom: '1px solid var(--border-subtle)',
      position: 'sticky',
      top: 0,
      zIndex: 5
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 10,
      color: 'var(--text-muted)',
      fontSize: 14,
      fontWeight: 600
    }
  }, /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 18
    }
  }, "\uD83D\uDC4B"), " Welcome back, ", u.name.split(' ')[0], "!"), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 12
    }
  }, /*#__PURE__*/React.createElement(StatPill, {
    kind: "xp",
    value: u.xp.toLocaleString(),
    label: "XP"
  }), /*#__PURE__*/React.createElement(StatPill, {
    kind: "streak",
    value: u.streak,
    label: "day streak"
  }), /*#__PURE__*/React.createElement(StatPill, {
    kind: "gems",
    value: u.gems
  }))), /*#__PURE__*/React.createElement("main", {
    style: {
      padding: 32,
      flex: 1
    }
  }, children)));
}
window.YK_AppShell = YK_AppShell;
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/app/AppShell.jsx", error: String((e && e.message) || e) }); }

// ui_kits/app/Dashboard.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
// Dashboard screen. window.YK_Dashboard
function YK_Dashboard({
  onStartQuiz
}) {
  const {
    Card,
    Button,
    Badge,
    ProgressBar,
    AchievementBadge
  } = window.DesignSystem_663a1e;
  const d = window.YK_DATA;
  const u = d.user;
  return /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      flexDirection: 'column',
      gap: 24,
      maxWidth: 1080
    }
  }, /*#__PURE__*/React.createElement(Card, {
    tone: "dark",
    pad: "none",
    style: {
      overflow: 'hidden'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'stretch'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      padding: '32px 36px',
      flex: 1
    }
  }, /*#__PURE__*/React.createElement("div", {
    className: "yk-eyebrow",
    style: {
      color: 'var(--yk-sunshine)'
    }
  }, "Daily Quest"), /*#__PURE__*/React.createElement("h2", {
    style: {
      color: '#fff',
      fontSize: 30,
      margin: '8px 0 6px'
    }
  }, "Ready for today's adventure?"), /*#__PURE__*/React.createElement("p", {
    style: {
      color: 'rgba(255,255,255,0.7)',
      maxWidth: 440,
      marginBottom: 20
    }
  }, "You're ", u.xpMax - u.xp, " XP from Level ", u.level + 1, ". Finish a quest to keep your ", u.streak, "-day streak alive!"), /*#__PURE__*/React.createElement("div", {
    style: {
      maxWidth: 360,
      marginBottom: 22
    }
  }, /*#__PURE__*/React.createElement(ProgressBar, {
    value: u.xp,
    max: u.xpMax,
    color: "sunshine",
    label: /*#__PURE__*/React.createElement("span", {
      style: {
        color: '#fff'
      }
    }, "Level ", u.level),
    showValue: true
  })), /*#__PURE__*/React.createElement(Button, {
    variant: "primary",
    size: "lg",
    onClick: onStartQuiz
  }, "Start daily quest \u2192")), /*#__PURE__*/React.createElement("div", {
    style: {
      width: 240,
      flex: 'none',
      position: 'relative',
      background: 'linear-gradient(160deg, var(--yk-teal-400), var(--yk-teal-600))',
      display: 'flex',
      alignItems: 'flex-end',
      justifyContent: 'center'
    }
  }, /*#__PURE__*/React.createElement("img", {
    src: "../../assets/mascots/boy-mascot.png",
    alt: "",
    style: {
      height: 280,
      marginBottom: -2
    }
  })))), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'baseline',
      marginBottom: 14
    }
  }, /*#__PURE__*/React.createElement("h3", {
    style: {
      margin: 0,
      fontSize: 22
    }
  }, "Your subjects"), /*#__PURE__*/React.createElement("a", {
    href: "#",
    style: {
      fontWeight: 700,
      fontSize: 14
    }
  }, "View all")), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'grid',
      gridTemplateColumns: 'repeat(4, 1fr)',
      gap: 16
    }
  }, d.subjects.map(s => /*#__PURE__*/React.createElement(Card, {
    key: s.id,
    interactive: true,
    pad: "md"
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 10,
      marginBottom: 14
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      width: 44,
      height: 44,
      borderRadius: 'var(--radius-md)',
      background: `var(--yk-${s.color}-100)`,
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      fontSize: 22
    }
  }, s.glyph), /*#__PURE__*/React.createElement("div", {
    style: {
      fontWeight: 700,
      fontSize: 16,
      color: 'var(--text-strong)'
    }
  }, s.name)), /*#__PURE__*/React.createElement(ProgressBar, {
    value: s.progress,
    max: 100,
    color: s.color,
    height: 10
  }), /*#__PURE__*/React.createElement("div", {
    style: {
      marginTop: 10,
      fontSize: 13,
      color: 'var(--text-muted)',
      fontWeight: 600
    }
  }, s.lessons, "/", s.total, " lessons"))))), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'grid',
      gridTemplateColumns: '1.6fr 1fr',
      gap: 24
    }
  }, /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("h3", {
    style: {
      fontSize: 22,
      marginBottom: 14
    }
  }, "Today's quests"), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      flexDirection: 'column',
      gap: 12
    }
  }, d.quests.map(q => /*#__PURE__*/React.createElement(Card, {
    key: q.id,
    pad: "sm",
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 16
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      width: 48,
      height: 48,
      borderRadius: 'var(--radius-md)',
      flex: 'none',
      background: `var(--yk-${q.color})`,
      color: q.color === 'sunshine' ? 'var(--yk-deepsea)' : '#fff',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      fontFamily: 'var(--font-display)',
      fontWeight: 800,
      fontSize: 20
    }
  }, q.done ? '✓' : q.title[0]), /*#__PURE__*/React.createElement("div", {
    style: {
      flex: 1,
      minWidth: 0
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 8
    }
  }, /*#__PURE__*/React.createElement("span", {
    style: {
      fontWeight: 700,
      color: 'var(--text-strong)',
      fontSize: 16
    }
  }, q.title), q.badge && /*#__PURE__*/React.createElement(Badge, {
    color: "pink",
    size: "sm"
  }, q.badge)), /*#__PURE__*/React.createElement("div", {
    style: {
      fontSize: 13,
      color: 'var(--text-muted)',
      fontWeight: 600
    }
  }, q.subject, " \xB7 ", q.mins, " min \xB7 +", q.xp, " XP")), q.done ? /*#__PURE__*/React.createElement(Badge, {
    color: "success",
    soft: true
  }, "Done") : /*#__PURE__*/React.createElement(Button, {
    variant: "teal",
    size: "sm",
    onClick: onStartQuiz
  }, "Start"))))), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("h3", {
    style: {
      fontSize: 22,
      marginBottom: 14
    }
  }, "Trophy case"), /*#__PURE__*/React.createElement(Card, {
    pad: "md"
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'grid',
      gridTemplateColumns: '1fr 1fr',
      gap: 16,
      justifyItems: 'center'
    }
  }, d.achievements.map((a, i) => /*#__PURE__*/React.createElement(AchievementBadge, _extends({
    key: i
  }, a))))))));
}
window.YK_Dashboard = YK_Dashboard;
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/app/Dashboard.jsx", error: String((e && e.message) || e) }); }

// ui_kits/app/Leaderboard.jsx
try { (() => {
// Leaderboard screen. window.YK_Leaderboard
function YK_Leaderboard() {
  const {
    Card,
    Avatar,
    Badge,
    StatPill
  } = window.DesignSystem_663a1e;
  const d = window.YK_DATA;
  const top3 = d.leaderboard.slice(0, 3);
  const podiumOrder = [top3[1], top3[0], top3[2]]; // 2,1,3
  const heights = {
    1: 132,
    2: 100,
    3: 84
  };
  const ringFor = {
    1: 'sunshine',
    2: 'teal',
    3: 'pink'
  };
  return /*#__PURE__*/React.createElement("div", {
    style: {
      maxWidth: 760,
      margin: '0 auto'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      textAlign: 'center',
      marginBottom: 8
    }
  }, /*#__PURE__*/React.createElement("h2", {
    style: {
      fontSize: 30,
      marginBottom: 4
    }
  }, "Weekly Leaderboard \uD83C\uDFC5"), /*#__PURE__*/React.createElement("p", {
    style: {
      color: 'var(--text-muted)'
    }
  }, "Top learners reset every Monday. Keep climbing!")), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'flex-end',
      gap: 16,
      margin: '24px 0 28px'
    }
  }, podiumOrder.map(p => /*#__PURE__*/React.createElement("div", {
    key: p.rank,
    style: {
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      width: 150
    }
  }, /*#__PURE__*/React.createElement(Avatar, {
    name: p.name,
    ring: ringFor[p.rank],
    size: p.rank === 1 ? 72 : 56
  }), /*#__PURE__*/React.createElement("div", {
    style: {
      fontWeight: 700,
      fontSize: 15,
      marginTop: 8,
      color: 'var(--text-strong)'
    }
  }, p.name.split(' ')[0]), /*#__PURE__*/React.createElement("div", {
    style: {
      fontSize: 13,
      color: 'var(--text-muted)',
      fontWeight: 600,
      marginBottom: 8
    }
  }, p.xp.toLocaleString(), " XP"), /*#__PURE__*/React.createElement("div", {
    style: {
      width: '100%',
      height: heights[p.rank],
      borderRadius: 'var(--radius-lg) var(--radius-lg) 0 0',
      background: p.rank === 1 ? 'var(--yk-sunshine)' : p.rank === 2 ? 'var(--yk-teal)' : 'var(--yk-pink)',
      color: p.rank === 1 ? 'var(--yk-deepsea)' : '#fff',
      display: 'flex',
      alignItems: 'flex-start',
      justifyContent: 'center',
      paddingTop: 12,
      fontFamily: 'var(--font-display)',
      fontWeight: 800,
      fontSize: 30,
      boxShadow: 'var(--shadow-md)'
    }
  }, p.rank)))), /*#__PURE__*/React.createElement(Card, {
    pad: "sm"
  }, d.leaderboard.map((p, i) => /*#__PURE__*/React.createElement("div", {
    key: p.rank,
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 14,
      padding: '12px 14px',
      borderRadius: 'var(--radius-md)',
      background: p.you ? 'var(--yk-pink-50)' : 'transparent',
      borderBottom: i < d.leaderboard.length - 1 ? '1px solid var(--border-subtle)' : 'none'
    }
  }, /*#__PURE__*/React.createElement("span", {
    style: {
      width: 28,
      textAlign: 'center',
      fontFamily: 'var(--font-display)',
      fontWeight: 800,
      fontSize: 18,
      color: p.rank <= 3 ? 'var(--yk-pink)' : 'var(--text-muted)'
    }
  }, p.rank), /*#__PURE__*/React.createElement(Avatar, {
    name: p.name,
    ring: p.ring,
    size: 40
  }), /*#__PURE__*/React.createElement("div", {
    style: {
      flex: 1,
      fontWeight: 700,
      color: 'var(--text-strong)'
    }
  }, p.name, " ", p.you && /*#__PURE__*/React.createElement(Badge, {
    color: "pink",
    size: "sm"
  }, "You")), /*#__PURE__*/React.createElement("span", {
    style: {
      fontFamily: 'var(--font-display)',
      fontWeight: 800,
      color: 'var(--yk-deepsea)'
    }
  }, p.xp.toLocaleString(), " ", /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 12,
      color: 'var(--text-muted)',
      fontWeight: 600
    }
  }, "XP"))))));
}
window.YK_Leaderboard = YK_Leaderboard;
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/app/Leaderboard.jsx", error: String((e && e.message) || e) }); }

// ui_kits/app/Lesson.jsx
try { (() => {
// LessonScreen — a lesson reading view with steps. window.YK_Lesson
function YK_Lesson({
  onStartQuiz
}) {
  const {
    Card,
    Button,
    Badge,
    ProgressBar
  } = window.DesignSystem_663a1e;
  const steps = [{
    done: true,
    label: 'What is a fraction?'
  }, {
    done: true,
    label: 'Numerator & denominator'
  }, {
    done: false,
    label: 'Adding like fractions',
    active: true
  }, {
    done: false,
    label: 'Practice challenge'
  }];
  return /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'grid',
      gridTemplateColumns: '260px 1fr',
      gap: 24,
      maxWidth: 1040,
      alignItems: 'start'
    }
  }, /*#__PURE__*/React.createElement(Card, {
    pad: "md"
  }, /*#__PURE__*/React.createElement("div", {
    className: "yk-eyebrow"
  }, "Mathematics"), /*#__PURE__*/React.createElement("h3", {
    style: {
      fontSize: 20,
      margin: '6px 0 16px'
    }
  }, "Fractions Frenzy"), /*#__PURE__*/React.createElement("div", {
    style: {
      marginBottom: 16
    }
  }, /*#__PURE__*/React.createElement(ProgressBar, {
    value: 2,
    max: 4,
    color: "teal",
    showValue: true,
    label: "Progress"
  })), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      flexDirection: 'column',
      gap: 4
    }
  }, steps.map((s, i) => /*#__PURE__*/React.createElement("div", {
    key: i,
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 12,
      padding: '10px 12px',
      borderRadius: 'var(--radius-md)',
      background: s.active ? 'var(--yk-teal-50)' : 'transparent'
    }
  }, /*#__PURE__*/React.createElement("span", {
    style: {
      width: 26,
      height: 26,
      flex: 'none',
      borderRadius: '50%',
      background: s.done ? 'var(--yk-success)' : s.active ? 'var(--yk-teal)' : 'var(--yk-ink-100)',
      color: s.done || s.active ? '#fff' : 'var(--text-faint)',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      fontWeight: 800,
      fontSize: 13
    }
  }, s.done ? '✓' : i + 1), /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 14,
      fontWeight: 600,
      color: s.active ? 'var(--yk-teal-700)' : s.done ? 'var(--text-body)' : 'var(--text-muted)'
    }
  }, s.label))))), /*#__PURE__*/React.createElement(Card, {
    pad: "lg"
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 10,
      marginBottom: 8
    }
  }, /*#__PURE__*/React.createElement(Badge, {
    color: "teal",
    soft: true
  }, "Step 3 of 4"), /*#__PURE__*/React.createElement(Badge, {
    color: "sunshine",
    size: "sm"
  }, "+50 XP")), /*#__PURE__*/React.createElement("h2", {
    style: {
      fontSize: 28,
      marginBottom: 14
    }
  }, "Adding like fractions"), /*#__PURE__*/React.createElement("p", {
    style: {
      fontSize: 17,
      color: 'var(--text-body)',
      maxWidth: 560
    }
  }, "When two fractions share the ", /*#__PURE__*/React.createElement("strong", null, "same denominator"), ", you only add the numerators \u2014 the bottom number stays the same. Think of slicing the same pizza: the slices are equal, so you just count how many you have."), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      gap: 16,
      alignItems: 'center',
      margin: '22px 0',
      flexWrap: 'wrap'
    }
  }, ['3/8', '+', '2/8', '=', '5/8'].map((t, i) => /*#__PURE__*/React.createElement("div", {
    key: i,
    style: {
      fontFamily: 'var(--font-display)',
      fontWeight: 800,
      fontSize: t.length > 1 ? 30 : 30,
      color: t === '=' || t === '+' ? 'var(--text-muted)' : 'var(--yk-deepsea)',
      background: t === '=' || t === '+' ? 'transparent' : 'var(--yk-sunshine-100)',
      padding: t === '=' || t === '+' ? 0 : '14px 22px',
      borderRadius: 'var(--radius-lg)'
    }
  }, t))), /*#__PURE__*/React.createElement(Card, {
    tone: "softteal",
    pad: "sm",
    style: {
      display: 'flex',
      gap: 12,
      alignItems: 'center',
      maxWidth: 560
    }
  }, /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 24
    }
  }, "\uD83D\uDCA1"), /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 15,
      fontWeight: 600,
      color: 'var(--yk-teal-700)'
    }
  }, "Tip: if the denominators are different, you'll need a common denominator first \u2014 that's the next lesson!")), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      gap: 12,
      marginTop: 26
    }
  }, /*#__PURE__*/React.createElement(Button, {
    variant: "outline"
  }, "\u2190 Back"), /*#__PURE__*/React.createElement(Button, {
    variant: "primary",
    onClick: onStartQuiz
  }, "Take the challenge \u2192"))));
}
window.YK_Lesson = YK_Lesson;
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/app/Lesson.jsx", error: String((e && e.message) || e) }); }

// ui_kits/app/Quiz.jsx
try { (() => {
// QuizScreen — interactive challenge. window.YK_Quiz
function YK_Quiz({
  onDone
}) {
  const {
    Card,
    Button,
    Badge,
    ProgressBar,
    OptionCard
  } = window.DesignSystem_663a1e;
  const React = window.React;
  const q = window.YK_DATA.quiz;
  const [picked, setPicked] = React.useState(null);
  const [locked, setLocked] = React.useState(false);
  const stateFor = opt => {
    if (!locked) return picked === opt.letter ? 'selected' : 'default';
    if (opt.correct) return 'correct';
    if (picked === opt.letter && !opt.correct) return 'wrong';
    return 'default';
  };
  const correct = locked && q.options.find(o => o.letter === picked)?.correct;
  return /*#__PURE__*/React.createElement("div", {
    style: {
      maxWidth: 640,
      margin: '0 auto'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 14,
      marginBottom: 18
    }
  }, /*#__PURE__*/React.createElement(Badge, {
    color: "teal",
    soft: true
  }, q.subject), /*#__PURE__*/React.createElement("div", {
    style: {
      flex: 1
    }
  }, /*#__PURE__*/React.createElement(ProgressBar, {
    value: q.qNo,
    max: q.qTotal,
    color: "teal",
    height: 12
  })), /*#__PURE__*/React.createElement("span", {
    style: {
      fontWeight: 700,
      color: 'var(--text-muted)',
      fontSize: 14
    }
  }, q.qNo, "/", q.qTotal)), /*#__PURE__*/React.createElement(Card, {
    pad: "lg"
  }, /*#__PURE__*/React.createElement("div", {
    className: "yk-eyebrow"
  }, "Question ", q.qNo), /*#__PURE__*/React.createElement("h2", {
    style: {
      fontSize: 30,
      margin: '8px 0 24px'
    }
  }, q.question), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      flexDirection: 'column',
      gap: 12
    }
  }, q.options.map(o => /*#__PURE__*/React.createElement(OptionCard, {
    key: o.letter,
    letter: o.letter,
    state: stateFor(o),
    disabled: locked,
    onClick: () => !locked && setPicked(o.letter)
  }, o.text))), locked && /*#__PURE__*/React.createElement(Card, {
    tone: correct ? 'softteal' : 'plain',
    pad: "sm",
    style: {
      marginTop: 18,
      display: 'flex',
      alignItems: 'center',
      gap: 12,
      background: correct ? 'var(--yk-success-soft)' : 'var(--yk-danger-soft)',
      border: 'none'
    }
  }, /*#__PURE__*/React.createElement("span", {
    style: {
      fontSize: 28
    }
  }, correct ? '🎉' : '💪'), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("div", {
    style: {
      fontWeight: 800,
      fontFamily: 'var(--font-display)',
      fontSize: 18,
      color: correct ? 'var(--yk-success)' : 'var(--yk-danger)'
    }
  }, correct ? 'Correct! +50 XP' : 'Not quite — keep going!'), /*#__PURE__*/React.createElement("div", {
    style: {
      fontSize: 14,
      color: 'var(--text-muted)'
    }
  }, "3/4 + 1/4 = 4/4 = 1 whole."))), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      justifyContent: 'flex-end',
      marginTop: 22
    }
  }, !locked ? /*#__PURE__*/React.createElement(Button, {
    variant: "primary",
    size: "lg",
    disabled: !picked,
    onClick: () => setLocked(true)
  }, "Check answer") : /*#__PURE__*/React.createElement(Button, {
    variant: "teal",
    size: "lg",
    onClick: onDone
  }, "Continue \u2192"))));
}
window.YK_Quiz = YK_Quiz;
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/app/Quiz.jsx", error: String((e && e.message) || e) }); }

// ui_kits/app/data.js
try { (() => {
// Yurka learning-app mock data
window.YK_DATA = {
  user: {
    name: 'Mira Adams',
    level: 7,
    xp: 640,
    xpMax: 1000,
    streak: 12,
    gems: 85,
    rank: 3
  },
  subjects: [{
    id: 'math',
    name: 'Mathematics',
    color: 'teal',
    glyph: '📐',
    progress: 72,
    lessons: 18,
    total: 25
  }, {
    id: 'science',
    name: 'Science',
    color: 'pink',
    glyph: '🧪',
    progress: 45,
    lessons: 9,
    total: 20
  }, {
    id: 'english',
    name: 'English',
    color: 'coral',
    glyph: '📖',
    progress: 88,
    lessons: 22,
    total: 25
  }, {
    id: 'history',
    name: 'History',
    color: 'sunshine',
    glyph: '🏛️',
    progress: 30,
    lessons: 6,
    total: 20
  }],
  quests: [{
    id: 1,
    title: 'Fractions Frenzy',
    subject: 'Mathematics',
    color: 'teal',
    xp: 50,
    mins: 8,
    done: false,
    badge: 'NEW'
  }, {
    id: 2,
    title: 'The Water Cycle',
    subject: 'Science',
    color: 'pink',
    xp: 40,
    mins: 6,
    done: false
  }, {
    id: 3,
    title: 'Story Structure',
    subject: 'English',
    color: 'coral',
    xp: 60,
    mins: 10,
    done: true
  }],
  leaderboard: [{
    rank: 1,
    name: 'Leo Park',
    xp: 2480,
    ring: 'sunshine',
    you: false
  }, {
    rank: 2,
    name: 'Aria Chen',
    xp: 2210,
    ring: 'teal',
    you: false
  }, {
    rank: 3,
    name: 'Mira Adams',
    xp: 1980,
    ring: 'pink',
    you: true
  }, {
    rank: 4,
    name: 'Noah Reed',
    xp: 1750,
    ring: 'coral',
    you: false
  }, {
    rank: 5,
    name: 'Zoe Müller',
    xp: 1610,
    ring: 'teal',
    you: false
  }, {
    rank: 6,
    name: 'Sam Okafor',
    xp: 1420,
    ring: 'sunshine',
    you: false
  }],
  achievements: [{
    glyph: '🚀',
    title: 'Fast Learner',
    color: 'teal',
    locked: false
  }, {
    glyph: '🔥',
    title: '7-Day Streak',
    color: 'coral',
    locked: false
  }, {
    glyph: '🏆',
    title: 'Quiz Champ',
    color: 'sunshine',
    locked: false
  }, {
    glyph: '🧠',
    title: 'Brainiac',
    color: 'pink',
    locked: true
  }],
  quiz: {
    subject: 'Mathematics',
    qNo: 3,
    qTotal: 5,
    question: 'What is 3/4 + 1/4?',
    options: [{
      letter: 'A',
      text: '1 / 2',
      correct: false
    }, {
      letter: 'B',
      text: '3 / 4',
      correct: false
    }, {
      letter: 'C',
      text: '1 whole',
      correct: true
    }, {
      letter: 'D',
      text: '2 / 8',
      correct: false
    }]
  }
};
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/app/data.js", error: String((e && e.message) || e) }); }

// ui_kits/marketing/Sections.jsx
try { (() => {
// Yurka marketing sections. window.YK_Marketing
function YK_Marketing() {
  const {
    Button,
    Badge,
    Card
  } = window.DesignSystem_663a1e;
  const features = [{
    glyph: '🎯',
    color: 'teal',
    title: 'Quests, not homework',
    body: 'Every lesson is a bite-sized quest with a clear goal, instant feedback, and XP rewards.'
  }, {
    glyph: '🔥',
    color: 'coral',
    title: 'Streaks that stick',
    body: 'Daily streaks and reminders build a study habit students actually want to keep.'
  }, {
    glyph: '🏅',
    color: 'pink',
    title: 'Climb the ranks',
    body: 'Weekly leaderboards and class tournaments turn revision into friendly competition.'
  }, {
    glyph: '🧠',
    color: 'sunshine',
    title: 'Real understanding',
    body: 'Adaptive challenges focus on the concepts each student finds tricky — not busywork.'
  }];
  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("nav", {
    style: {
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      padding: '20px 48px',
      maxWidth: 1200,
      margin: '0 auto'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 10
    }
  }, /*#__PURE__*/React.createElement("img", {
    src: "../../assets/logo/yurka-mark.svg",
    alt: "",
    style: {
      height: 36
    }
  }), /*#__PURE__*/React.createElement("img", {
    src: "../../assets/logo/yurka-wordmark-dark.svg",
    alt: "Yurka",
    style: {
      height: 24
    }
  })), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 28
    }
  }, ['How it works', 'Subjects', 'For schools', 'Pricing'].map(l => /*#__PURE__*/React.createElement("a", {
    key: l,
    href: "#",
    style: {
      color: 'var(--text-body)',
      fontWeight: 600,
      fontSize: 15
    }
  }, l)), /*#__PURE__*/React.createElement(Button, {
    variant: "primary",
    size: "sm"
  }, "Start free"))), /*#__PURE__*/React.createElement("section", {
    style: {
      maxWidth: 1200,
      margin: '0 auto',
      padding: '40px 48px 0',
      display: 'grid',
      gridTemplateColumns: '1fr 0.85fr',
      gap: 32,
      alignItems: 'center'
    }
  }, /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(Badge, {
    color: "teal",
    soft: true,
    icon: /*#__PURE__*/React.createElement("span", null, "\u2728")
  }, "For middle & high school"), /*#__PURE__*/React.createElement("h1", {
    style: {
      fontSize: 60,
      lineHeight: 1.02,
      letterSpacing: '-0.02em',
      margin: '18px 0 18px'
    }
  }, "Learning that feels like ", /*#__PURE__*/React.createElement("span", {
    style: {
      color: 'var(--yk-teal)'
    }
  }, "playing"), "."), /*#__PURE__*/React.createElement("p", {
    style: {
      fontSize: 20,
      color: 'var(--text-body)',
      maxWidth: 480,
      marginBottom: 28
    }
  }, "Yurka turns studying into an adventure. Quests, streaks, points and friends \u2014 so students learn, play, compete and grow at the same time."), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      gap: 14,
      alignItems: 'center'
    }
  }, /*#__PURE__*/React.createElement(Button, {
    variant: "primary",
    size: "lg"
  }, "Start free \u2192"), /*#__PURE__*/React.createElement(Button, {
    variant: "outline",
    size: "lg",
    iconLeft: /*#__PURE__*/React.createElement("span", null, "\u25B6")
  }, "Watch demo")), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      gap: 28,
      marginTop: 32
    }
  }, [['2M+', 'students'], ['40+', 'subjects'], ['4.9★', 'app rating']].map(([n, l]) => /*#__PURE__*/React.createElement("div", {
    key: l
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      fontFamily: 'var(--font-display)',
      fontWeight: 800,
      fontSize: 28,
      color: 'var(--yk-deepsea)'
    }
  }, n), /*#__PURE__*/React.createElement("div", {
    style: {
      fontSize: 14,
      color: 'var(--text-muted)',
      fontWeight: 600
    }
  }, l))))), /*#__PURE__*/React.createElement("div", {
    style: {
      position: 'relative',
      display: 'flex',
      justifyContent: 'center'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      position: 'absolute',
      inset: '10% 8% 0',
      background: 'radial-gradient(circle at 50% 40%, var(--yk-sunshine-200), transparent 70%)',
      borderRadius: '50%'
    }
  }), /*#__PURE__*/React.createElement("img", {
    src: "../../assets/mascots/girl-mascot.png",
    alt: "Yurka mascot",
    style: {
      height: 460,
      position: 'relative',
      filter: 'drop-shadow(0 24px 32px rgba(10,37,64,0.18))'
    }
  }))), /*#__PURE__*/React.createElement("section", {
    style: {
      maxWidth: 1200,
      margin: '0 auto',
      padding: '64px 48px'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      textAlign: 'center',
      marginBottom: 40
    }
  }, /*#__PURE__*/React.createElement("div", {
    className: "yk-eyebrow"
  }, "Why Yurka works"), /*#__PURE__*/React.createElement("h2", {
    style: {
      fontSize: 40,
      margin: '8px 0 0'
    }
  }, "Built for how Gen\xA0Alpha learns")), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'grid',
      gridTemplateColumns: 'repeat(4, 1fr)',
      gap: 20
    }
  }, features.map(f => /*#__PURE__*/React.createElement(Card, {
    key: f.title,
    interactive: true,
    pad: "md",
    style: {
      display: 'flex',
      flexDirection: 'column',
      gap: 12
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      width: 56,
      height: 56,
      borderRadius: 'var(--radius-lg)',
      background: `var(--yk-${f.color}-100)`,
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      fontSize: 28
    }
  }, f.glyph), /*#__PURE__*/React.createElement("h3", {
    style: {
      fontSize: 19,
      margin: 0
    }
  }, f.title), /*#__PURE__*/React.createElement("p", {
    style: {
      fontSize: 15,
      color: 'var(--text-muted)',
      margin: 0
    }
  }, f.body))))), /*#__PURE__*/React.createElement("section", {
    style: {
      maxWidth: 1200,
      margin: '0 auto 72px',
      padding: '0 48px'
    }
  }, /*#__PURE__*/React.createElement(Card, {
    tone: "dark",
    pad: "none",
    style: {
      overflow: 'hidden'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'grid',
      gridTemplateColumns: '1fr auto',
      alignItems: 'center'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      padding: '48px 56px'
    }
  }, /*#__PURE__*/React.createElement("h2", {
    style: {
      color: '#fff',
      fontSize: 40,
      marginBottom: 12
    }
  }, "Ready to make studying fun?"), /*#__PURE__*/React.createElement("p", {
    style: {
      color: 'rgba(255,255,255,0.72)',
      fontSize: 18,
      maxWidth: 440,
      marginBottom: 26
    }
  }, "Join millions of students leveling up every day. Free to start \u2014 no credit card needed."), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      gap: 14
    }
  }, /*#__PURE__*/React.createElement(Button, {
    variant: "primary",
    size: "lg"
  }, "Start free \u2192"), /*#__PURE__*/React.createElement(Button, {
    variant: "ghost",
    size: "lg",
    style: {
      color: '#fff'
    }
  }, "For schools"))), /*#__PURE__*/React.createElement("img", {
    src: "../../assets/mascots/boy-mascot.png",
    alt: "",
    style: {
      height: 300,
      marginRight: 40,
      marginBottom: -4,
      alignSelf: 'flex-end'
    }
  })))), /*#__PURE__*/React.createElement("footer", {
    style: {
      background: 'var(--surface-card)',
      borderTop: '1px solid var(--border-subtle)'
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      maxWidth: 1200,
      margin: '0 auto',
      padding: '40px 48px',
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'center',
      flexWrap: 'wrap',
      gap: 20
    }
  }, /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      alignItems: 'center',
      gap: 10
    }
  }, /*#__PURE__*/React.createElement("img", {
    src: "../../assets/logo/yurka-mark.svg",
    alt: "",
    style: {
      height: 30
    }
  }), /*#__PURE__*/React.createElement("img", {
    src: "../../assets/logo/yurka-wordmark-dark.svg",
    alt: "Yurka",
    style: {
      height: 20
    }
  })), /*#__PURE__*/React.createElement("div", {
    style: {
      display: 'flex',
      gap: 24,
      color: 'var(--text-muted)',
      fontSize: 14,
      fontWeight: 600
    }
  }, /*#__PURE__*/React.createElement("a", {
    href: "#"
  }, "About"), /*#__PURE__*/React.createElement("a", {
    href: "#"
  }, "Subjects"), /*#__PURE__*/React.createElement("a", {
    href: "#"
  }, "For schools"), /*#__PURE__*/React.createElement("a", {
    href: "#"
  }, "Help"), /*#__PURE__*/React.createElement("a", {
    href: "#"
  }, "Privacy")), /*#__PURE__*/React.createElement("div", {
    style: {
      color: 'var(--text-faint)',
      fontSize: 13
    }
  }, "\xA9 2026 Yurka. Learn, play, grow."))));
}
window.YK_Marketing = YK_Marketing;
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/marketing/Sections.jsx", error: String((e && e.message) || e) }); }

__ds_ns.Avatar = __ds_scope.Avatar;

__ds_ns.Badge = __ds_scope.Badge;

__ds_ns.Button = __ds_scope.Button;

__ds_ns.Card = __ds_scope.Card;

__ds_ns.Input = __ds_scope.Input;

__ds_ns.OptionCard = __ds_scope.OptionCard;

__ds_ns.AchievementBadge = __ds_scope.AchievementBadge;

__ds_ns.ProgressBar = __ds_scope.ProgressBar;

__ds_ns.StatPill = __ds_scope.StatPill;

})();
