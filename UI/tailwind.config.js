/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.tsx'],
  theme: {
    screens: {
      sm: '640px',
      md: '768px',
      lg: '1024px',
      xl: '1280px'
    },
    extend: {
      colors: {
        zombieBlack: '#050606',
        zombieBlue: '#1E2DDE',
        zombieGray: '#A5AAA8',
        zombieRed: '#D62A05',
        zombieWhite: '#E6FAF3'
      }
    },
  },
  plugins: [],
}
