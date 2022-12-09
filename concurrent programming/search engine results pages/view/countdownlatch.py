from threading import Condition


class CountDownLatch:

    def __init__(self, count = 5) -> None:
        self.count = count
        self.condition = Condition()

    def set_count(self, count):
        self.count = count

    def countdown(self):
        with self.condition:
            if self.count == 0:
                return
            self.count -= 1
            if self.count == 0:
                self.condition.notify_all()

    def wait(self):
        with self.condition:
            if self.count == 0:
                return
            self.condition.wait()