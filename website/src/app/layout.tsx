// import './styles/main.scss';
import { Barlow_Semi_Condensed } from 'next/font/google';
import ClientLayout from './ClientLayout';

const barlow = Barlow_Semi_Condensed({
  subsets: ['latin'],
  weight: ['400', '700'],
  variable: '--font-barlow',
});

export const siteMetadata  = {
  title: 'kanboom',
  description: 'An open-source kanban app',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en" className={barlow.variable}>
      <body>
        <ClientLayout>{children}</ClientLayout>
      </body>
    </html>
  );
}