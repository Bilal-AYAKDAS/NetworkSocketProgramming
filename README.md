\documentclass{article}

\begin{document}

\title{TCP Sunucu ve İstemci Uygulaması Açıklamaları}
\author{Bilal}
\date{\today}
\maketitle

\section{Giriş}
Bu doküman, C\# dilinde yazılmış bir TCP sunucu ve istemci uygulamasının açıklamalarını içermektedir. Sunucu ve istemci uygulamaları, TCP protokolü kullanarak asenkron çoklu iş parçacıklı bir iletişim sağlar. Sunucu, bir istemciden gelen bağlantıları kabul eder ve gelen mesajları diğer tüm bağlı istemcilere yayınlar. İstemci, sunucuya bağlanır ve kullanıcıdan alınan girdiyi sunucuya gönderir.

\section{Sunucu (Server) Kodu}
Sunucu uygulaması, istemcilerden gelen bağlantıları kabul eden ve alınan mesajları tüm bağlı istemcilere gönderen bir TCP sunucusu olarak çalışır.

\subsection{Kod Açıklamaları}

\begin{itemize}
  \item \textbf{InitializeComponent} metodu, Windows Form'un bileşenlerini başlatır ve sunucu arayüzünü oluşturur. Bu arayüz, kullanıcıdan metin girişi almak ve mesajları göstermek için bir \texttt{TextBox}, \texttt{ListBox} ve iki \texttt{Button} içerir.
  \item \textbf{ButtonListenOnClick} metodu, kullanıcı "Listen" düğmesine tıkladığında tetiklenir. Sunucu soketini başlatır, belirtilen IP adresi ve port üzerinde dinlemeye başlar ve yeni istemci bağlantılarını kabul eder.
  \item \textbf{AcceptConn} metodu, bir istemci bağlantısı kabul edildiğinde çağrılır. Yeni istemci soketini ekler ve veri alımını başlatır.
  \item \textbf{ReceiveData} metodu, istemciden gelen verileri almak için kullanılır. Alınan verileri kullanıcı arayüzünde gösterir ve mesajı diğer istemcilere gönderir.
  \item \textbf{SendData} metodu, sunucu tarafından istemciye veri gönderilmesini sağlar.
\end{itemize}

\section{İstemci (Client) Kodu}
İstemci uygulaması, kullanıcıdan metin girişi alır ve bu veriyi sunucuya gönderir. Aynı zamanda sunucudan gelen mesajları da alır ve gösterir.

\subsection{Kod Açıklamaları}

\begin{itemize}
  \item \textbf{InitializeComponent} metodu, Windows Form'un bileşenlerini başlatır ve istemci arayüzünü oluşturur. Bu arayüz, kullanıcıdan metin girişi almak ve mesajları göstermek için bir \texttt{TextBox}, \texttt{ListBox} ve iki \texttt{Button} içerir.
  \item \textbf{ButtonConnectOnClick} metodu, kullanıcı "Connect" düğmesine tıkladığında tetiklenir. İstemci soketini başlatır ve belirtilen IP adresi ve port'a bağlanır.
  \item \textbf{ButtonSendOnClick} metodu, kullanıcı "Send" düğmesine tıkladığında tetiklenir. Kullanıcının girdiği metni sunucuya gönderir.
  \item \textbf{ReceiveData} metodu, sunucudan gelen verileri almak için kullanılır. Alınan verileri kullanıcı arayüzünde gösterir.
\end{itemize}

\end{document}
