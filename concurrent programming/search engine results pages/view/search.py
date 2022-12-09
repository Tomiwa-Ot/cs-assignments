import operator
import requests
import justext
from threading import Lock
from serpapi import GoogleSearch
from PyQt5.QtCore import QThreadPool, QRunnable, pyqtSlot
from PyQt5.QtWidgets import QFrame, QHBoxLayout, QPushButton, QLineEdit

from view.countdownlatch import CountDownLatch


class Search(QFrame):

    def __init__(self, data, visual) -> None:
        super().__init__()

        self.search_links = []
        self.search_content = {}
        self.search_scores = {}
        self.latch = CountDownLatch()
        self.lock = Lock()

        self.threadpool = QThreadPool()
        self.data = data
        self.visual = visual

        self.search_button = QPushButton('Go')
        self.search_button.setFixedHeight(35)
        self.search_button.clicked.connect(self.perform_query)

        self.text_field = QLineEdit()
        self.text_field.setPlaceholderText('Search...')
        self.text_field.setFixedHeight(35)

        hbox = QHBoxLayout()
        hbox.addWidget(self.text_field)
        hbox.addWidget(self.search_button)

        self.setLayout(hbox)

    def perform_query(self):
        params = {
            "engine": "google",
            "q": self.text_field.text(),
            "api_key": "SERPAPI API KEY HERE"
        }
        search = GoogleSearch(params)
        response = search.get_dict()
        self.latch.set_count(len(response['organic_results']))
        for res in response['organic_results']:
            self.search_links.append(res['link'])
            worker = SearchWorker(self.text_field.text(), res['title'], res['link'], self.search_content, self.search_scores, self.lock, self.latch)
            self.threadpool.start(worker)
        self.latch.wait()
        scores_sum = 0
        for key in self.search_scores:
            scores_sum += self.search_scores[key]
        for key in self.search_scores:
            self.search_scores[key] = (self.search_scores[key] / scores_sum) * 100
        sorted_results = self.sort_scores()
        print(sorted_results)
        self.visual.update(sorted_results)
        result = {}
        for key in self.sort_scores():
            result[key] = self.search_content[key]
        self.data.update(result)
        self.search_links = []
        self.search_content = {}
        self.search_scores = {}

    def sort_scores(self):
        not_sorted = dict(sorted(self.search_scores.items(), key=operator.itemgetter(1), reverse=True))
        val = {}
        for i, key in enumerate(not_sorted):
            val[key] = not_sorted[key]
            if i == 4:
                break
        return val
        


class SearchWorker(QRunnable):

    def __init__(self, query, title, link, content_dict, score_dict, lock, latch) -> None:
        super().__init__()
        self.query = query
        self.title = title
        self.link = link
        self.content_dict = content_dict
        self.score_dict = score_dict
        self.latch = latch
        self.lock = lock
        self.content = ''

    @pyqtSlot()
    def run(self):
        try:
            req = requests.get(self.link)
            paragraphs = justext.justext(req.content, justext.get_stoplist('English'))
            for paragraph in paragraphs:
                if not paragraph.is_boilerplate:
                    self.content += str(paragraph.text)
            self.lock.acquire()
            self.content_dict[self.title] = self.content.replace('\n', '')
            self.score_dict[self.title] = self.term_frequency()
            self.lock.release()
        except:
            pass
        finally:
            self.latch.countdown()

    def term_frequency(self):
        words = self.content.split(' ')
        term_length = 0
        for word in words:
            if word.lower() in self.query.lower().split(' '):
                term_length += 1
        return (term_length / len(words))